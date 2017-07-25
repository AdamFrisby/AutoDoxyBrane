using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace AutoDoxyBrane
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectName = "Your API";
            string projectDesc = "Your API for autocompletion";
            string shortcode = "yourapi";
            string rootClass = "rootClass";
            string rootLibName = "YourAPI";

            string dir = "T:\\trunk\\documentation\\scripting\\xml";
            string output = "T:\\trunk\\documentation\\scripting\\yourapi.lua";

            string wrapper =
                "local interpreter = {\r\n  name = \"%projname%\",\r\n  description = \"%projdesc%\",\r\n  api = { \"baselib\", \"%shortcode%\" },\r\n  fattachdebug = function(self) DebuggerAttachDefault() end,\r\n  skipcompile = true,\r\n}\r\n\r\nlocal api = %APIDEFINITION%\r\n\r\n\r\nreturn {\r\n  name = \"%projname%\",\r\n  description = \"%projdesc%\",\r\n  onRegister = function(self)\r\n    ide:AddAPI(\"lua\", \"%shortcode%\", api)\r\n    ide:AddInterpreter(\"%shortcode%\", interpreter)\r\n  end,\r\n  onUnRegister = function(self)\r\n    ide:RemoveAPI(\"lua\", \"%shortcode%\")\r\n    ide:RemoveInterpreter(\"%shortcode%\")\r\n  end,\r\n}";

            var files = Directory.GetFiles(dir, "*.xml");

            List<string> members = new List<string>();

            foreach (var file in files)
            {
                string fn = Path.GetFileName(file);
                if (fn.StartsWith("class_") || fn.StartsWith("struct_"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Doxygen));
                    using (TextReader reader = new StringReader(Sanitise(File.ReadAllText(file))))
                    {
                        Doxygen result = (Doxygen)serializer.Deserialize(reader);

                        members.Add(GenerateTable(result, rootClass, rootLibName));
                    }
                }
            }
            
            var contents = wrapper
                            .Replace("%APIDEFINITION%", "{ \n" + string.Concat(members) + "}")
                            .Replace("%projname%", projectName)
                            .Replace("%projdesc%", projectDesc)
                            .Replace("%shortcode%", shortcode);

            File.WriteAllText(output, contents);
        }

        private static string Sanitise(string file)
        {
            Regex r = new Regex("(<ref .*\">)", RegexOptions.Compiled);

            var matches = r.Matches(file);
            foreach (Match match in matches)
            {
                file = file.Replace(match.Value, "");
            }

            return file.Replace("</ref>", "");
        }

        private static string GenerateTable(Doxygen doxy, string rootClass, string rootLibName)
        {
            if ((doxy.Compounddef.Kind == "class" || doxy.Compounddef.Kind == "struct") && doxy.Compounddef.Prot == "public")
            {
                List<string> children = new List<string>();

                string name = doxy.Compounddef.Compoundname;

                var sections = doxy.Compounddef.Sectiondef;
                foreach (var section in sections)
                {
                    foreach (var member in section.Memberdef)
                    {
                        if (member.Prot != "public")
                            continue;

                        if (member.Kind == "property")
                        {
                            children.Add("\n\t\t\t" + member.Name + " = { type = \"value\", valuetype=\"" + member.Type + "\"},");
                        }
                        else if (member.Kind == "variable") // consts
                        {
                            string type = member.Definition;

                            if (string.IsNullOrEmpty(type) || (!string.IsNullOrEmpty(member.Type) && !member.Type.Contains(" ")))
                                type = member.Type;

                            if (type.Contains(" "))
                                type = type.Split(' ')[1];

                            children.Add("\n\t\t\t" + member.Name + " = { type = \"value\", valuetype=\"" + type + "\"},");
                        }
                        else if (member.Kind == "function")
                        {
                            if (member.Name.StartsWith("operator"))
                                continue;

                            string type = member.Type;
                            if (type == "void")
                                type = "nil";

                            children.Add("\n\t\t\t" + member.Name + " = { type=\"function\", " +
                                         "args =\"" + member.Argsstring + "\", " +
                                         "returns =\"(" + type + ")\", " +
                                         "valuetype =\"" + type + "\", " +
                                         "},");
                        }
                    }
                }
                
                string t = "class";

                if (name == rootClass)
                {
                    t = "lib";
                    name = rootLibName;
                }

                return "\t" + name + " = {\n\t\ttype=\"" + t + "\",\n\t\tchilds= {" + string.Join("", children) + "\n\t\t}\n\t},\n";
            }

            return "";
        }
    }
}

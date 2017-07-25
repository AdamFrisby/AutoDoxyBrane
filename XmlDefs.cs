using System.Xml.Serialization;
using System.Collections.Generic;

namespace AutoDoxyBrane
{
    [XmlRoot(ElementName = "ref")]
    public class Ref
    {
        [XmlAttribute(AttributeName = "refid")]
        public string Refid { get; set; }
        [XmlAttribute(AttributeName = "kindref")]
        public string Kindref { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "type")]
    public class Type
    {
        [XmlElement(ElementName = "ref")]
        public Ref Ref { get; set; }
    }

    [XmlRoot(ElementName = "location")]
    public class Location
    {
        [XmlAttribute(AttributeName = "file")]
        public string File { get; set; }
        [XmlAttribute(AttributeName = "line")]
        public string Line { get; set; }
        [XmlAttribute(AttributeName = "column")]
        public string Column { get; set; }
        [XmlAttribute(AttributeName = "bodyfile")]
        public string Bodyfile { get; set; }
        [XmlAttribute(AttributeName = "bodystart")]
        public string Bodystart { get; set; }
        [XmlAttribute(AttributeName = "bodyend")]
        public string Bodyend { get; set; }
    }

    [XmlRoot(ElementName = "memberdef")]
    public class Memberdef
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "definition")]
        public string Definition { get; set; }
        [XmlElement(ElementName = "argsstring")]
        public string Argsstring { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        //[XmlElement(ElementName = "briefdescription")]
        //public string Briefdescription { get; set; }
        //[XmlElement(ElementName = "detaileddescription")]
        //public string Detaileddescription { get; set; }
        //[XmlElement(ElementName = "inbodydescription")]
        //public string Inbodydescription { get; set; }
        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }
        [XmlAttribute(AttributeName = "kind")]
        public string Kind { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "prot")]
        public string Prot { get; set; }
        [XmlAttribute(AttributeName = "static")]
        public string Static { get; set; }
        [XmlAttribute(AttributeName = "mutable")]
        public string Mutable { get; set; }
        [XmlAttribute(AttributeName = "readable")]
        public string Readable { get; set; }
        [XmlAttribute(AttributeName = "writable")]
        public string Writable { get; set; }
        [XmlAttribute(AttributeName = "gettable")]
        public string Gettable { get; set; }
        [XmlAttribute(AttributeName = "privategettable")]
        public string Privategettable { get; set; }
        [XmlAttribute(AttributeName = "protectedgettable")]
        public string Protectedgettable { get; set; }
        [XmlAttribute(AttributeName = "settable")]
        public string Settable { get; set; }
        [XmlAttribute(AttributeName = "privatesettable")]
        public string Privatesettable { get; set; }
        [XmlAttribute(AttributeName = "protectedsettable")]
        public string Protectedsettable { get; set; }
        [XmlAttribute(AttributeName = "const")]
        public string Const { get; set; }
        [XmlAttribute(AttributeName = "explicit")]
        public string Explicit { get; set; }
        [XmlAttribute(AttributeName = "inline")]
        public string Inline { get; set; }
        [XmlAttribute(AttributeName = "virt")]
        public string Virt { get; set; }
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; }
    }

    [XmlRoot(ElementName = "sectiondef")]
    public class Sectiondef
    {
        [XmlElement(ElementName = "memberdef")]
        public List<Memberdef> Memberdef { get; set; }
        [XmlAttribute(AttributeName = "kind")]
        public string Kind { get; set; }
    }

    [XmlRoot(ElementName = "param")]
    public class Param
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "declname")]
        public string Declname { get; set; }
    }

    [XmlRoot(ElementName = "member")]
    public class Member
    {
        [XmlElement(ElementName = "scope")]
        public string Scope { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "refid")]
        public string Refid { get; set; }
        [XmlAttribute(AttributeName = "prot")]
        public string Prot { get; set; }
        [XmlAttribute(AttributeName = "virt")]
        public string Virt { get; set; }
    }

    [XmlRoot(ElementName = "listofallmembers")]
    public class Listofallmembers
    {
        [XmlElement(ElementName = "member")]
        public List<Member> Member { get; set; }
    }

    [XmlRoot(ElementName = "compounddef")]
    public class Compounddef
    {
        [XmlElement(ElementName = "compoundname")]
        public string Compoundname { get; set; }
        [XmlElement(ElementName = "sectiondef")]
        public List<Sectiondef> Sectiondef { get; set; }
        [XmlElement(ElementName = "briefdescription")]
        public string Briefdescription { get; set; }
        [XmlElement(ElementName = "detaileddescription")]
        public string Detaileddescription { get; set; }
        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }
        [XmlElement(ElementName = "listofallmembers")]
        public Listofallmembers Listofallmembers { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "kind")]
        public string Kind { get; set; }
        [XmlAttribute(AttributeName = "language")]
        public string Language { get; set; }
        [XmlAttribute(AttributeName = "prot")]
        public string Prot { get; set; }
    }

    [XmlRoot(ElementName = "doxygen")]
    public class Doxygen
    {
        [XmlElement(ElementName = "compounddef")]
        public Compounddef Compounddef { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string NoNamespaceSchemaLocation { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

}

using System.Xml;
using System.Xml.Schema;

namespace PensionAccumulationCalculator.Repos {
    internal static class XmlValidator {
        public static void Validate(string xml, string xsd) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var schema = XmlSchema.Read(new StringReader(xsd), ValidationCallBack) 
                ?? throw new XmlSchemaValidationException("Xml Schema was not found");
            doc.Schemas.Add(schema);

            doc.Validate(ValidationCallBack);
        }

        public static void Validate(XmlDocument xml, string xsd) {
            var schema = XmlSchema.Read(new StreamReader(xsd), ValidationCallBack)
                ?? throw new XmlSchemaValidationException();
            xml.Schemas.Add(schema);

            xml.Validate(ValidationCallBack);
        }

        private static void ValidationCallBack(object? sender, ValidationEventArgs args) {
            if (args.Severity == XmlSeverityType.Error) {
                throw new XmlSchemaValidationException();
            }
        }
    }
}
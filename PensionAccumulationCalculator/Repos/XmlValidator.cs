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

        public static void Validate(ICollection<string> xmls, string xsd) {
            var schemas = new XmlSchemaSet();
            var schema = XmlSchema.Read(new StringReader(xsd), ValidationCallBack) 
                ?? throw new XmlSchemaValidationException("Xml Schema was not found");
            schemas.Add(schema);

            foreach (var xml in xmls) {
                var doc = new XmlDocument();

                doc.LoadXml(xml);
                doc.Schemas = schemas;
                doc.Validate(ValidationCallBack);
            }
        }

        private static void ValidationCallBack(object? sender, ValidationEventArgs args) {
            if (args.Severity == XmlSeverityType.Error) {
                throw new XmlSchemaValidationException();
            }
        }
    }
}

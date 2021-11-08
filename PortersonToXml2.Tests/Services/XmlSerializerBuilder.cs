using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace PortersonToXml2.Services
{
    public class XmlSerializerBuilder
    {
        private readonly XmlAttributeOverrides Overrides = new();

        public XmlSerializerBuilder Type<T>(Action<TypeConfiguration<T>> config)
        {
            config(new TypeConfiguration<T>(Overrides));

            return this;
        }

        public class TypeConfiguration<T>
        {
            private readonly XmlAttributeOverrides Overrides;

            public TypeConfiguration(XmlAttributeOverrides overrides)
            {
                Overrides = overrides;
            }

            public TypeConfiguration<T> Name(string xmlName)
            {
                Overrides.Add(typeof(T), new XmlAttributes
                {
                    XmlType = new XmlTypeAttribute(xmlName)
                });

                return this;
            }

            public TypeConfiguration<T> AttributeMember<TMember>(Expression<Func<T, TMember>> member, string xmlAttributeName, string? dataType = null)
            {
                var propertyName = ExtractPropertyName(member);
                var attributes = new XmlAttributes();

                attributes.XmlAttribute = new XmlAttributeAttribute(xmlAttributeName);

                if (dataType != null)
                {
                    attributes.XmlAttribute.DataType = dataType;
                }

                Overrides.Add(typeof(T), propertyName, attributes);

                return this;
            }

            public TypeConfiguration<T> ElementMember<TMember>(Expression<Func<T, TMember>> member, string xmlElementName, string? dataType = null)
            {
                var propertyName = ExtractPropertyName(member);
                var attributes = new XmlAttributes();

                if (typeof(TMember) != typeof(string) && typeof(TMember).IsAssignableTo(typeof(IEnumerable)))
                {
                    attributes.XmlArray = new XmlArrayAttribute(xmlElementName);
                }
                else
                {
                    var attribute = new XmlElementAttribute(xmlElementName);

                    if (dataType != null)
                    {
                        attribute.DataType = dataType;
                    }

                    attributes.XmlElements.Add(attribute);
                }

                Overrides.Add(typeof(T), propertyName, attributes);

                return this;
            }

            public TypeConfiguration<T> ContentsMember<TMember>(Expression<Func<T, TMember>> member, string xmlItemElementsName) where TMember : IEnumerable
            {
                var propertyName = ExtractPropertyName(member);
                var attributes = new XmlAttributes();

                attributes.XmlElements.Add(new XmlElementAttribute(xmlItemElementsName));

                Overrides.Add(typeof(T), propertyName, attributes);

                return this;
            }

            private static string ExtractPropertyName(LambdaExpression member)
            {
                if (member.Body.NodeType != ExpressionType.MemberAccess)
                {
                    throw new ArgumentException("`member` must be a property access expression.", nameof(member));
                }

                var accessedMember = ((MemberExpression) member.Body).Member;

                if (accessedMember.MemberType != MemberTypes.Property)
                {
                    throw new ArgumentException("`member` must be a property access expression.", nameof(member));
                }

                return accessedMember.Name;
            }
        }

        public XmlSerializer BuildFor<T>()
        {
            return new XmlSerializer(typeof(T), Overrides);
        }
    }
}

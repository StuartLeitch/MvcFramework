// Copied by Stuart from Microsoft Linq Samples C:\Program Files (x86)\Microsoft Visual Studio 10.0\Samples\1033\CSharpSamples.zip\LinqSamples\ObjectDumper
// also available at http://code.msdn.microsoft.com/cs2008samples/Release/ProjectReleases.aspx?ReleaseId=8

//Copyright (C) Microsoft Corporation.  All rights reserved.

// See the ReadMe.html for additional information

using System.Linq;
using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Infrastructure.Core.TestHelpers
{
    public class ObjectDumper
    {
        public static void Write(object element, bool addNewLine = false)
        {
            Write(element, 0, addNewLine);
        }

        public static void Write(object element, int depth, bool addNewLine)
        {
            Write(element, depth, Console.Out, addNewLine);
        }

        public static void Write(object element, int depth, TextWriter log, bool addNewLine)
        {
            ObjectDumper dumper = new ObjectDumper(depth);
            dumper.writer = log;
            dumper.WriteObject(null, element, addNewLine);
        }

        TextWriter writer;
        int pos;
        int level;
        int depth;

        private ObjectDumper(int depth)
        {
            this.depth = depth;
        }

        private void Write(string s)
        {
            if (s != null)
            {
                this.writer.Write(s);
                this.pos += s.Length;
            }
        }

        private void WriteIndent()
        {
            for (int i = 0; i < this.level; i++) this.writer.Write("  ");
        }

        private void WriteLine()
        {
            this.writer.WriteLine();
            this.pos = 0;
        }

        private void WriteTab()
        {
            this.Write("  ");
            while (this.pos % 8 != 0) this.Write(" ");
        }

        private void WriteObject(string prefix, object element, bool addNewLine)
        {
            if (element == null || element is ValueType || element is string)
            {
                this.WriteIndent();
                this.Write(prefix);
                this.WriteValue(element);
                this.WriteLine();
            }
            else
            {
                IEnumerable enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            this.WriteIndent();
                            this.Write(prefix);
                            this.Write("...");
                            this.WriteLine();
                            if (this.level < this.depth)
                            {
                                this.level++;
                                this.WriteObject(prefix, item, addNewLine);
                                this.level--;
                            }
                        }
                        else
                        {
                            this.WriteObject(prefix, item, addNewLine);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    this.WriteIndent();
                    this.Write(prefix);
                    bool propWritten = false;
                    foreach (MemberInfo m in members)
                    {
                        FieldInfo f = m as FieldInfo;
                        PropertyInfo p = m as PropertyInfo;
                        if (f != null || p != null)
                        {
                            if (propWritten)
                            {
                                this.WriteTab();
                            }
                            else
                            {
                                propWritten = true;
                            }
                            this.Write(m.Name);
                            this.Write("=");
                            Type t = f != null ? f.FieldType : p.PropertyType;
                            if (t.IsValueType || t == typeof(string))
                            {
                                this.WriteValue(f != null ? f.GetValue(element) : p.GetValue(element, null));
                            }
                            else
                            {
                                if (typeof(IEnumerable).IsAssignableFrom(t))
                                {
                                    this.Write("...");
                                }
                                else
                                {
                                    this.Write("{ }");
                                }
                            }

                            if (addNewLine)
                                this.WriteLine();
                        }
                    }
                    if (propWritten) this.WriteLine();
                    if (this.level < this.depth)
                    {
                        foreach (MemberInfo m in members)
                        {
                            FieldInfo f = m as FieldInfo;
                            PropertyInfo p = m as PropertyInfo;
                            if (f != null || p != null)
                            {
                                Type t = f != null ? f.FieldType : p.PropertyType;
                                if (!(t.IsValueType || t == typeof(string)))
                                {
                                    object value = f != null ? f.GetValue(element) : p.GetValue(element, null);
                                    if (value != null)
                                    {
                                        this.level++;
                                        this.WriteObject(m.Name + ": ", value, addNewLine);
                                        this.level--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteValue(object o)
        {
            if (o == null)
            {
                this.Write("null");
            }
            else if (o is DateTime)
            {
                this.Write(((DateTime)o).ToShortDateString());
            }
            else if (o is ValueType || o is string)
            {
                this.Write(o.ToString());
            }
            else if (o is IEnumerable)
            {
                this.Write("...");
            }
            else
            {
                this.Write("{ }");
            }
        }
    }
}
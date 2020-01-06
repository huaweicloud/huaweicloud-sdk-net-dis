using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Ext
{
    public class Properties : Dictionary<object, object>
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetProperty(string key, string value)
        {
            this[key] = value;
        }

        public string GetProperty(string key)
        {
            object oval;
            if (!TryGetValue(key, out oval))
            {
                return null;
            }

            if (oval is string)
            {
                return (string) oval;
            }

            return null;
        }

        public string GetProperty(string key, string defaultValue)
        {
            var value = GetProperty(key);
            return value ?? defaultValue;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Load(StreamReader reader)
        {
            LoadFile(reader);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Load(FileStream stream)
        {
            var streamReader = new StreamReader(stream);
            LoadFile(streamReader);
        }

        private void LoadFile(StreamReader streamReader)
        {
            var convtBuf = new char[1024];

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                var limit = line.Length;
                var keyLen = 0;
                var valueStart = limit;
                var hasSep = false;

                var precedingBackslash = false;
                while (keyLen < limit)
                {
                    var c = line[keyLen];
                    //need check if escaped.
                    if ((c == '=' ||  c == ':') && !precedingBackslash) {
                        valueStart = keyLen + 1;
                        hasSep = true;
                        break;
                    }
                    if ((c == ' ' || c == '\t' ||  c == '\f') && !precedingBackslash) {
                        valueStart = keyLen + 1;
                        break;
                    }
                    if (c == '\\') {
                        precedingBackslash = !precedingBackslash;
                    } else {
                        precedingBackslash = false;
                    }
                    keyLen++;
                }

                while (valueStart < limit)
                {
                    var c = line[valueStart];
                    if (c != ' ' && c != '\t' &&  c != '\f') {
                        if (!hasSep && (c == '=' ||  c == ':')) {
                            hasSep = true;
                        } else {
                            break;
                        }
                    }
                    valueStart++;
                }

                var key = LoadConvert(line.ToCharArray(), 0, keyLen, convtBuf);
                var value = LoadConvert(line.ToCharArray(), valueStart, limit - valueStart, convtBuf);
                SetProperty(key, value);
            }
        }

        private string LoadConvert(char[] input, int offset, int length, char[] convtBuf)
        {
            if (convtBuf.Length < length)
            {
                var newLen = length * 2;
                if (newLen < 0)
                {
                    newLen = int.MaxValue;
                }
                convtBuf = new char[newLen];
            }
            var output = convtBuf;
            var outLen = 0;
            var end = offset + length;

            while (offset < end)
            {
                var aChar = input[offset++];
                if (aChar == '\\')
                {
                    aChar = input[offset++];
                    if(aChar == 'u')
                    {
                        // Read the xxxx
                        var value = 0;
                        for (var i=0; i < 4; i++)
                        {
                            aChar = input[offset++];
                            switch (aChar) {
                              case '0': case '1': case '2': case '3': case '4':
                              case '5': case '6': case '7': case '8': case '9':
                                 value = (value << 4) + aChar - '0';
                                 break;
                              case 'a': case 'b': case 'c':
                              case 'd': case 'e': case 'f':
                                 value = (value << 4) + 10 + aChar - 'a';
                                 break;
                              case 'A': case 'B': case 'C':
                              case 'D': case 'E': case 'F':
                                 value = (value << 4) + 10 + aChar - 'A';
                                 break;
                              default:
                                  throw new ArgumentException("Malformed \\uxxxx encoding.");
                            }
                        }
                        output[outLen++] = (char) value;
                    }
                    else
                    {
                        if (aChar == 't')
                            aChar = '\t';
                        else if (aChar == 'r')
                            aChar = '\r';
                        else if (aChar == 'n')
                            aChar = '\n';
                        else if (aChar == 'f')
                            aChar = '\f';

                        output[outLen++] = aChar;
                    }
                } 
                else 
                {
                    output[outLen++] = aChar;
                }
            }
            return new string(output, 0, outLen);
        }
    }
}

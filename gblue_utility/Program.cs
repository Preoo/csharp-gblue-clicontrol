using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;

namespace gblue_utility
{
    public class InputArguments
    {
        #region fields & properties
        public const string DEFAULT_KEY_LEADING_PATTERN = "-";

        protected Dictionary<string, string> _parsedArguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        protected readonly string _keyLeadingPattern;

        public string this[string key]
        {
            get { return GetValue(key); }
            set
            {
                if (key != null)
                    _parsedArguments[key] = value;
            }
        }
        public string KeyLeadingPattern
        {
            get { return _keyLeadingPattern; }
        }
        #endregion

        #region public methods
        public InputArguments(string[] args, string keyLeadingPattern)
        {
            _keyLeadingPattern = !string.IsNullOrEmpty(keyLeadingPattern) ? keyLeadingPattern : DEFAULT_KEY_LEADING_PATTERN;

            if (args != null && args.Length > 0)
                Parse(args);
        }
        public InputArguments(string[] args)
            : this(args, null)
        {
        }

        public bool Contains(string key)
        {
            string adjustedKey;
            return ContainsKey(key, out adjustedKey);
        }

        public virtual string GetPeeledKey(string key)
        {
            return IsKey(key) ? key.Substring(_keyLeadingPattern.Length) : key;
        }
        public virtual string GetDecoratedKey(string key)
        {
            return !IsKey(key) ? (_keyLeadingPattern + key) : key;
        }
        public virtual bool IsKey(string str)
        {
            return str.StartsWith(_keyLeadingPattern);
        }
        #endregion

        #region internal methods
        protected virtual void Parse(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null) continue;

                string key = null;
                string val = null;

                if (IsKey(args[i]))
                {
                    key = args[i];

                    if (i + 1 < args.Length && !IsKey(args[i + 1]))
                    {
                        val = args[i + 1];
                        i++;
                    }
                }
                else
                    val = args[i];

                // adjustment
                if (key == null)
                {
                    key = val;
                    val = null;
                }
                _parsedArguments[key] = val;
            }
        }

        protected virtual string GetValue(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
                return _parsedArguments[adjustedKey];

            return null;
        }

        protected virtual bool ContainsKey(string key, out string adjustedKey)
        {
            adjustedKey = key;

            if (_parsedArguments.ContainsKey(key))
                return true;

            if (IsKey(key))
            {
                string peeledKey = GetPeeledKey(key);
                if (_parsedArguments.ContainsKey(peeledKey))
                {
                    adjustedKey = peeledKey;
                    return true;
                }
                return false;
            }

            string decoratedKey = GetDecoratedKey(key);
            if (_parsedArguments.ContainsKey(decoratedKey))
            {
                adjustedKey = decoratedKey;
                return true;
            }
            return false;
        }
        #endregion
    }
    public class UtilityArguments : InputArguments
    {
        /// <summary>
        /// Class to extend inputarguments
        /// </summary>
        public string Channel
        {
            get { return GetValue("c"); }
        }

        public string Url
        {
            get { return GetValue("gb"); }
        }

        public bool Help
        {
            get { return GetBoolValue("-h"); }
        }

        public string Zap
        {
            get { return GetValue("z"); }
        }

        public bool Reboot
        {
            get { return GetBoolValue("-reboot"); }
        }

        public UtilityArguments(string[] args)
            : base(args)
        {
        }

        protected bool GetBoolValue(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                bool res;
                bool.TryParse(_parsedArguments[adjustedKey], out res);
                return res;
            }
            return false;
        }
    }
    public class gblueControl
    {
        /// <summary>
        /// Class for controlling box and getting channel information.
        /// TODO: add functionality from main() to here.
        /// Utilityarguments.Zap to use with -z
        /// 
        /// </summary>
        
        
    }
    public class printer
    {
        /// <summary>
        /// Class for giving information to user.
        /// TODO: partial keywords
        /// </summary>
        private static string[] nippuA = new string[] { "yle tv1", "yle tv2", "yle teema", "yle fem", "fox", "ava" };
        private static string[] nippuB = new string[] { "mtv3", "nelonen", "sub", "liv", "mtv max", "mtv juniori", "mtv leffa", "estradi" };
        private static string[] nippuC = new string[] { "tv5", "hero", "huvi2", "nelonen pro 1", "nelonen pro 2" };
        private static string[] nippuE = new string[] { "jim", "huvi1", "nelonen prime", "nelonen maailma", "nelonen nappula" };
        //private string[] niputyhteen = new string[nippuA.Length + nippuB.Length + nippuC.Length + nippuE.Length];
        public string availableTrans ( string cha) 
        {
            if (nippuA.Contains(cha))
            {
                string a = "Voit katsoa kanavia: " + string.Join(" / ", nippuA);
                //string a ="Voit katsoa kanavia: Yle TV1, Yle TV2, Yle Teema, Yle Fem, FOX AVA";
                return a;
            }
            if (nippuB.Contains(cha))
            {
                string a = "Voit katsoa kanavia: " + string.Join(" / ", nippuB);
                //string a = "Voit katsoa kanavia: MTV3, Nelonen, Sub, Liv, MTV Max, MTV Juniori, MTV Leffa, Estradi";
                return a;
            }
            if (nippuC.Contains(cha))
            {
                string a = "Voit katsoa kanavia: " + string.Join(" / ", nippuC);
                //string a = "Voit katsoa kanavia: TV5, Hero, Huvi2, Nelonen Pro 1, Nelonen Pro 2";
                return a;
            }
            if (nippuE.Contains(cha))
            {
                string a = "Voit katsoa kanavia: " + string.Join(" / ", nippuE);
                //string a = "Voit katsoa kanavia: Jim, Nelonen Prime, Nelonen Maailma, Nelonen Nappula";
                return a;
            }
            else
            {
                string a = "Viritin on vapaana käyttöön.";
                return a;
            }
            
        }
        public string print_arraymergetest() 
        {
            var newlist = new List<string>();
            newlist.AddRange(nippuA);
            newlist.AddRange(nippuB);
            newlist.AddRange(nippuC);
            newlist.AddRange(nippuE);
            string[] newlistarray = newlist.ToArray();
            string s = string.Join(",", newlistarray);
            return s;
        }
        
        // Check if input matches multiple channels
        public bool checkDubChannels (string cha_to_check)
        {
            
            string niputlist = print_arraymergetest();
            string[] niputlistarray = niputlist.Split(',');
            string[] matchedchannelsarray = Array.FindAll(niputlistarray, element => element.Contains(cha_to_check));
            if (matchedchannelsarray.Length > 1)
            {

                foreach (string kanava in matchedchannelsarray)
                {
                    Console.WriteLine(kanava);
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        
    }
    class gblue
    {

        static void Main(string[] args)
        {
            UtilityArguments arguments = new UtilityArguments(args);
            if (arguments.Contains("h"))
            {
                Console.WriteLine("Usage: -c <channel name(string)> -gb <blank for default> or <http://<gbquad_ip>/web/subservice> -h <print this information> -z <zap to sRef>,requires -gb switch -reboot,requires -gb switch");
            }
            if (arguments.Contains("c"))
            {
                // method to check if multible channels excists and prompt for more specific one. Do until clearly one option remains.
                printer dubcheck  = new printer();
                bool test = dubcheck.checkDubChannels(arguments.Channel.ToLower());
                if (!test)
                {
                    //printer print_con_cha_arg = new printer();
                    //Console.WriteLine(print_con_cha_arg.availableTrans(arguments.Channel.ToLower()));
                    Console.WriteLine(dubcheck.availableTrans(arguments.Channel.ToLower()));
                }
                else
                {
                    Console.WriteLine("Match found for multiple channels, retry query for specific channel from this list");
                }
            }

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: -c <channel name(string)> -gb <blank for default> or <http://<gbquad_host>/web/subservice> -h <print this information> -z <zap to sRef>,requires -gb switch -reboot,requires -gb switch");
                Console.WriteLine("");
                printer debug_printer = new printer();
                Console.WriteLine(debug_printer.print_arraymergetest().ToString());
                Console.ReadKey();
                return;
            }
            if (arguments.Contains("gb"))
            {
                XmlDocument xmldoc = new XmlDocument();
                try
                {
                    string custompath = "http://" + arguments.Url + "/web/subservices";
                    Console.WriteLine(custompath);
                    xmldoc.Load(custompath);
                    //xmldoc.Load("http://" + arguments.Url + "/web/subservices");
                    goto usertrydefault;
                }
                catch
                {
                    // default URL for GBlue.
                    xmldoc.Load("http://192.168.1.59/web/subservices");
                    Console.WriteLine("Fault in address, using defaults ");
                    goto usertrydefault;
                }
            usertrydefault:
                XmlNode userNode = xmldoc.DocumentElement.SelectSingleNode("/e2servicelist/e2service/e2servicename");
                string cha = userNode.InnerText;
                XmlNode srefNode = xmldoc.DocumentElement.SelectSingleNode("/e2servicelist/e2service/e2servicereference");
                string sref = srefNode.InnerText;


                Console.WriteLine("");
                Console.WriteLine("GBlue address: " + arguments.Url);
                Console.WriteLine("Test Channel: " + arguments.Channel);
                Console.WriteLine("Channel sRef: " + sref);
                Console.WriteLine("Current channel: " + cha);
                Console.WriteLine("");

                #region edit
                printer print_con = new printer();
                Console.WriteLine(print_con.availableTrans(cha.ToLower()));
                #endregion
                ///Console.WriteLine("Help: -c <channel name(string)> -gb <"http://<gbquad_ip>/web/subservice"> -h <print this information>");
                /// Todo
                /// - rewrite for better modularity
                /// - add functionality to zap from tool
                /// 
                Console.WriteLine("");
                if (arguments.Contains("z"))
                {
                    try
                    {
                        string sUrl = "http://gbquad/web/zap?sRef=" + sref;
                        
                        Console.WriteLine(sUrl);
                        using (var wb = new WebClient())
                        {
                            var response = wb.DownloadString(sUrl);
                            Console.WriteLine(response.ToString());
                            Console.WriteLine("");
                            Console.WriteLine("Zapping issued, check for results.");
                        }
                    }
                    catch (WebException e)
                    {

                        Console.WriteLine("Error :" + e.Message);
                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                            Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                            Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                // Reboot gbquad -gb -reboot
                
                if (arguments.Contains("reboot"))
                {
                    try
                    {
                        string s2Url = "http://gbquad/web/powerstate?newstate=2";

                        Console.WriteLine(s2Url);
                        using (var wb2 = new WebClient())
                        {
                            var response2 = wb2.DownloadString(s2Url);
                            Console.WriteLine(response2.ToString());
                            Console.WriteLine("");
                            Console.WriteLine("Rebooting ...");
                        }
                        Console.WriteLine("Rebooting...");
                    }
                    catch (WebException e)
                    {

                        Console.WriteLine("Error :" + e.Message);
                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                            Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                            Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            Console.ReadKey();
        }
    }
 
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear_logInfo();

            string instructionString = textBox1.Text;

            if (String.IsNullOrEmpty(instructionString) || instructionString.Contains("Input"))
            {
                MessageBox.Show("Click the [Help] button :)");
            }
            else
            {
                minmax_test(instructionString);
            }
        }

        private void clear_logInfo()
        {
            richTextBox1.Text = "";
        }

        private void minmax_test(string instructionString)
        {
            char[] separators = new char[] { ',', '-' };

            Dictionary<string, Node_MinMax> allNodes = new Dictionary<string, Node_MinMax>();

            var tokens = instructionString.Split(';');

            string nodeName = "";
            int nodeCost = -1;

            string rootNode = "A"; //default value

            try
            {
                //Creating the nodes here
                foreach (var token in tokens)
                {
                    if (token.Contains('='))
                    {
                        var subtokens = token.Split('=');

                        nodeName = subtokens[0].Trim();

                        nodeCost = Convert.ToInt32(subtokens[1].Trim());

                        Node_MinMax nod_nou = new Node_MinMax(nodeCost, nodeName);

                        allNodes.Add(nod_nou.name, nod_nou);

                    }
                }

                //Creating the node relations here
                foreach (var token in tokens)
                {
                    if (token.Contains('-'))
                    {
                        var subtokens = token.Split(separators);

                        nodeName = subtokens[0].Trim();

                        if (nodeName == "root")
                        {
                            rootNode = subtokens[1].Trim();
                        }
                        else
                        {

                            if (!allNodes.ContainsKey(nodeName))
                            {
                                Node_MinMax newNode = new Node_MinMax(nodeName);
                                allNodes.Add(nodeName, newNode);
                            }

                            var parentNode = allNodes[nodeName];

                            for (int i = 1; i < subtokens.Length; i++)
                            {
                                var childNodeName = subtokens[i].Trim();

                                if (childNodeName.Length > 0)
                                {
                                    if(!allNodes.ContainsKey(childNodeName))
                                    {
                                        Node_MinMax newNode = new Node_MinMax(childNodeName);
                                        allNodes.Add(childNodeName, newNode);
                                    }

                                    var childNode = allNodes[childNodeName];

                                    parentNode.addChild(childNode);
                                }
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                string displayMessage = "Exception thrown when processing the input string. Maybe click the [Help] button and try one of the input strings there ? Exception details :";

                displayMessage += ex.Message;


                MessageBox.Show(displayMessage);
            }

            var root = allNodes[rootNode];

            minmax(root, true, 0);

            logInfo(0, "");

            logInfo(0, "Final values of all nodes :");

            print_final_values(root, true);

        }

        void logInfo(int recursionLevel,string str)
        {
            str= str.Replace(Node_MinMax.infinit.ToString(), "infinit");
            str = str.Replace(Node_MinMax.unknownCost.ToString(), "unknown");
            str = str.Replace("isMaxLevel=True", "level=MAX");
            str = str.Replace("isMaxLevel=False", "level=MIN");

            for (int i=0;i<recursionLevel;i++)
            {
                richTextBox1.Text += "    ";
            }

            richTextBox1.Text += str + Environment.NewLine;
        }

        public string displayList(List<string> lista)
        {
            string rezultat = "[";

            if (lista != null)
            {
                foreach (var element in lista)
                {
                    rezultat += element + ",";
                }

                rezultat = rezultat.Substring(0,rezultat.Length-1);
            }

            rezultat += "]";

            return rezultat;
        }

        void print_final_values(Node_MinMax nod, bool isMaxLevel)
        {
            string level = "MAX";

            int valoare = 0;

            if (isMaxLevel)
            {
                if(nod.cost==Node_MinMax.unknownCost)
                {
                    valoare = nod.alpha;
                }
                else
                {
                    valoare = nod.cost;
                }

                if(Math.Abs(valoare)==Node_MinMax.infinit)
                {
                    valoare = Node_MinMax.unknownCost;
                }

                logInfo(0,string.Format("{0}={1} (Alpha={2},Beta={3},Cost={4},Level={5})", nod.name, valoare, nod.alpha, nod.beta, nod.cost, level));

            }
            else
            {
                level = "MIN";

                if (nod.cost == Node_MinMax.unknownCost)
                {
                    valoare = nod.beta;
                }
                else
                {
                    valoare = nod.cost;
                }

                if (Math.Abs(valoare) == Node_MinMax.infinit)
                {
                    valoare = Node_MinMax.unknownCost;
                }

                logInfo(0, string.Format("{0}={1} (Alpha={2},Beta={3},Cost={4},Level={5})", nod.name, valoare, nod.alpha, nod.beta,nod.cost, level));

            }

            foreach(var childnode in nod.children)
            {
                print_final_values(childnode, !isMaxLevel);
            }
        }

        int minmax(Node_MinMax nod, bool isMaxLevel, int recursionLevel)
        {
            if(nod.cost != Node_MinMax.unknownCost)
            {
                logInfo(recursionLevel,string.Format("Reached node {0} and returned cost {1}", nod.name, nod.cost));

                return nod.cost;
            }
            else
            {
                if(isMaxLevel)
                {
                    //Initializam cu -infinit
                    int best = -Node_MinMax.infinit;

                    var valori = new List<string>();

                    logInfo(recursionLevel,string.Format("Started processing node {0} with Alpha={1} and Beta={2}, isMaxLevel={3}", nod.name, nod.alpha, nod.beta, isMaxLevel));

                    int contor_index = 0;

                    foreach (var child in nod.children)
                    {
                        int valoare = minmax(child, !isMaxLevel, recursionLevel+1);

                        valori.Add(child.name+"="+valoare);

                        best = Math.Max(best, valoare);

                        nod.alpha = Math.Max(nod.alpha, best);

                        //Alpha beta pruning
                        if(nod.beta <= nod.alpha)
                        {
                            for (contor_index++; contor_index < nod.children.Count; contor_index++)
                            {
                                logInfo(recursionLevel, string.Format("Pruned node {0} because Beta={1} <= Alpha={2}", nod.children[contor_index].name, nod.beta, nod.alpha));
                            }

                            break;
                        }

                        //Not sure about this part
                        foreach (var childnode in nod.children)
                        {
                            if (childnode.alpha < nod.alpha)
                            {
                                logInfo(recursionLevel, string.Format("From parent node {0}, changed child node {1} Alpha value from {2} to {3}", nod.name, childnode.name, childnode.alpha, nod.alpha));
                                childnode.alpha = Math.Max(childnode.alpha, nod.alpha);
                            }
                        }

                        contor_index++;
                    }

                    logInfo(recursionLevel,string.Format("Found child nodes: {0}", displayList(valori)));

                    logInfo(recursionLevel,string.Format("Stopped processing node {0} , new Alpha={1} and Beta={2} isMaxLevel={3}", nod.name, nod.alpha, nod.beta, isMaxLevel));

                    logInfo(recursionLevel,"*********************************");


                    return best;
                }
                else
                {
                    int best = Node_MinMax.infinit;

                    var valori = new List<string>();

                    logInfo(recursionLevel,string.Format("Started processing node {0} with Alpha={1} and Beta={2}, isMaxLevel={3}", nod.name, nod.alpha, nod.beta, isMaxLevel));

                    int contor_index = 0;

                    foreach (var child in nod.children)
                    {
                        int valoare = minmax(child, !isMaxLevel,recursionLevel+1);

                        valori.Add(child.name + "=" + valoare);

                        best = Math.Min(best, valoare);

                        nod.beta = Math.Min(nod.beta, best);

                        //Alpha beta pruning
                        if (nod.beta <= nod.alpha)
                        {
                            for (contor_index++; contor_index < nod.children.Count; contor_index++)
                            {
                                logInfo(recursionLevel, string.Format("Pruned node {0} because Beta={1} <= Alpha={2}", nod.children[contor_index].name, nod.beta, nod.alpha));
                            }

                            break;
                        }

                        //Not sure about this part
                        foreach (var childnode in nod.children)
                        {
                            if (childnode.beta > nod.beta)
                            {
                                logInfo(recursionLevel, string.Format("From parent node {0}, changed child node {1} Beta value from {2} to {3}", nod.name, childnode.name, childnode.beta, nod.beta));
                                childnode.beta = Math.Min(childnode.beta, nod.beta);
                            }
                        }
                        contor_index++;
                    }
                    

                    logInfo(recursionLevel,string.Format("Found child nodes: {0}", displayList(valori)));

                    logInfo(recursionLevel,string.Format("Stopped processing node {0} , new Alpha={1} and Beta={2}, isMaxLevel={3}", nod.name, nod.alpha, nod.beta, isMaxLevel));

                    logInfo(recursionLevel,"*********************************");


                    return best;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear_logInfo();

            string sampleInstruction = "Q=1;R=10;K=3;L=0;N=7;O=2;P=20;E=2;H=2;I=1;M - Q,R; F - K,L; G - M,N; J - O,P; B - E,F; C - G,H; D - I,J; A - B,C,D; root-A"; 

            logInfo(0, "Copy one of the following input strings into the [Input Field] then click the [Solve] button ! (You can also  create your own input string) : ");

            logInfo(0, "");

            textBox1.Text = sampleInstruction;

            logInfo(0, "Input string #1 : " + sampleInstruction);

            logInfo(0, "Input string #2 : H=3;I=5;J=6;K=9;L=1;M=2;N=0;O=0; D-H,I; E-J,K; F-L,M; G-N,O; B-D,E; C-F,G; A-B,C; root-A");

            logInfo(0, ""); logInfo(0, ""); logInfo(0, "");

            logInfo(0, "To create your own input string, first add the node values separated by \";\" character ( e.g : E=7; F=5; G=3; ) ");
            logInfo(0, "Then add the node relations ; ( e.g : A-B,C,D; B-E,F; C-G; )  (This means that A has children B,C,D. B has children E,F. C has children G.");
            logInfo(0, "You can also decide which node should be the root, try typing root-A or root-B at the end");

            logInfo(0, "Try out this input string :  E=7; F=5; G=3; A-B,C,D; B-E,F; C-G; root-A");

        }
    }
}

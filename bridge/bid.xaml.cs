using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bridge
{
    /// <summary>
    /// Interaction logic for bid.xaml
    /// </summary>
    public partial class Bid : Window
    {

        List<Button> buttons = new List<Button>();
        List<List<string>> bids = new List<List<string>>();

        public Bid()
        {
            InitializeComponent();

            buttons.Add(b00);
            buttons.Add(b01);
            buttons.Add(b02);
            buttons.Add(b03);
            buttons.Add(b04);
            buttons.Add(b05);
            buttons.Add(b06);
            buttons.Add(b07);
            buttons.Add(b08);
            buttons.Add(b09);
            buttons.Add(b10);
            buttons.Add(b11);
            buttons.Add(b12);
            buttons.Add(b13);
            buttons.Add(b14);
            buttons.Add(b15);
            buttons.Add(b16);
            buttons.Add(b17);
            buttons.Add(b18);
            buttons.Add(b19);
            buttons.Add(b20);
            buttons.Add(b21);
            buttons.Add(b22);
            buttons.Add(b23);
            buttons.Add(b24);
            buttons.Add(b25);
            buttons.Add(b26);
            buttons.Add(b27);
            buttons.Add(b28);
            buttons.Add(b29);
            buttons.Add(b30);
            buttons.Add(b31);
            buttons.Add(b32);
            buttons.Add(b33);
            buttons.Add(b34);

            Random r = new Random();
            int start = r.Next(0, 4);
            if (start == 0)
            {
                vulnerability.Text += "\nDealer\tW";
                Computer_Move(MainWindow.W);
                Computer_Move(MainWindow.N);
                Computer_Move(MainWindow.E);
            }
            else if (start == 1)
            {
                vulnerability.Text += "\nDealer\tN";
                Computer_Move(MainWindow.N);
                Computer_Move(MainWindow.E);
            }
            else if (start == 2)
            {
                vulnerability.Text += "\nDealer\tE";
                Computer_Move(MainWindow.E);
            }
            else
                vulnerability.Text += "\nDealer\tS";

            start = r.Next(0, 4);
            if (start == 0)
                vulnerability.Text += "\nbrak";
            else if (start == 1)
                vulnerability.Text += "\nNS";
            else if (start == 2)
                vulnerability.Text += "\nEW";
            else
                vulnerability.Text += "\nNS+EW";





        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //szukanie przycisku
            int counter = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] == sender)
                {
                    counter = i;
                    break;
                }

            }

            if (sender != X_Double)
            {
                //zablokowanie niższych odzywek
                for (int i = 0; i <= counter; i++)
                    buttons[i].IsEnabled = false;

                //rejestrowanie przebiegu rozgrywki
                course.Text += buttons[counter].Content + " ";
                List<string> tmp = new List<string>();
                tmp.Add("S");
                tmp.Add(buttons[counter].Content.ToString());
                bids.Add(tmp);
            }
            else
            {
                List<string> tmp = new List<string>();
                tmp.Add("S");
                tmp.Add("X");
                course.Text += "X ";
                bids.Add(tmp);
            }

            if (Computer_Move(MainWindow.W))
            {
                MessageBox.Show("koniec");
                this.Close();
            }
            if (Computer_Move(MainWindow.N))
            {
                MessageBox.Show("koniec");
                this.Close();
            }
            if (Computer_Move(MainWindow.E))
            {
                MessageBox.Show("koniec");
                this.Close();
            }

            //licznik kiedy nie pas +3 == ewaluiacja


        }


        bool Is_Possible(string bid)
        {
            if (bid == "PAS" || bid == "X")
                return true;
            if (bids.Count == 0)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Content.ToString() == bid)
                    {
                        buttons[i].IsEnabled = false;
                        break;
                    }
                    buttons[i].IsEnabled = false;
                }

            }

            if (bids.Count != 0 && bids[bids.Count - 1][1] == bid) 
                return false;

            if (bids.Count > 2)
                if (bids[bids.Count - 1][1] == bid || bids[bids.Count - 2][1] == bid || bids[bids.Count - 3][1] == bid)
                    return false;

           
            int counter = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Content.ToString() == bid)
                {
                    counter = i;
                    buttons[i].IsEnabled = false;
                    break;
                }
                buttons[i].IsEnabled = false;
            }


            if (buttons[counter + 1].IsEnabled == false)
                return false;
            return true;
        }

        string Graph1(Player x)
        {
            string result = "PAS";
            if (x.dist.HP < 6)
            {
                x.inf.maxHP = 5;
                return result;
            }

            if (x.dist.HP < 12)
            {
                string color = Find_Longest_Color(x);
                if (color == "C" && x.dist.C >= 6)
                {
                    x.inf.maxD = 5;
                    return "2C";
                }
                if (color == "D" && x.dist.D >= 6)
                {
                    return "2D";
                }
                if (color == "H" && x.dist.H >= 6)
                {
                    return "2H";
                }
                if (color == "S" && x.dist.S >= 6)
                {
                    return "2S";
                }
                x.inf.maxHP = 11;
                return "PAS";
            }

            x.inf.minHP = 12;
            if (x.dist.H >= 5 || x.dist.S >= 5)
            {
                string tmp = "";
                if (x.dist.H >= 5 && x.dist.S < 5)
                {
                    tmp = "H";
                    x.inf.minH = 5;
                    x.inf.maxS = 4;
                }
                if (x.dist.S >= 5)
                {
                    tmp = "S";
                    x.inf.minS = 5;
                }
                return "1" + tmp;
            }
            else
            {
                x.inf.maxH = 4;
                x.inf.maxS = 4;
                string tmp = Find_Longest_Color(x, false);
                if (tmp == "C")
                    x.inf.minC = 3;
                else
                    x.inf.minD = 3;
                return "1" + tmp;
            }

        }

        string Graph2(Player x, string bid)
        {

            string result = "PAS";
            int points = x.dist.HP;
            int counter = 0;
            if (points < 6)
                return "PAS";
            if (points < 10)
                counter = 1;
            else if (points < 13)
                counter = 2;
            else if (points < 18)
                counter = 3;
            else
                counter = 4;


            if (bid[0] == 'X')
            {
                if (Is_5(x))
                    return counter + Find_Longest_Color(x);
                else
                    return counter + "BA";
            }

            if (bid[0] == '2')
            {
                if (points < 13)
                    result = "PAS";
                else
                    result = "2BA";
            }
            else
            {
                if (bid[1] == 'S')
                {
                    if (x.dist.S >= 3)
                        result = (counter + 1).ToString() + "S";
                    else if (Is_5(x))
                        result = counter + Find_Longest_Color(x);
                    else
                        result = counter + "BA";
                }
                else if(bid[1] == 'H')
                {
                    if (x.dist.H >= 3)
                        result = (counter + 1).ToString() + "H";
                    else if (Is_5(x))
                        result = counter + Find_Longest_Color(x);
                    else
                        result = counter + "BA";
                }
                else if (bid[1] == 'D')
                {
                    if (Is_5(x))
                        result = counter + Find_Longest_Color(x);
                    else if (x.dist.D >= 4)
                        result = (counter + 1).ToString() + "D";
                    else
                        result = counter + "BA";
                }
                else if (bid[1] == 'C')
                {
                    if (Is_5(x))
                        result = counter + Find_Longest_Color(x);
                    else if (x.dist.C >= 4)
                        result = (counter + 1).ToString() + "C";
                    else
                        result = counter + "BA";
                }

            }
            return result;
        }

        bool Computer_Move(Player x)
        {
            List<string> tmp = new List<string>();
            tmp.Add(x.index);
            string result = "";
         
            if (bids.Count < 2 || bids[bids.Count - 2][1] == "PAS")
            {
                result = Graph1(x);
                if (!Is_Possible(result))
                    result = "X";
            }
            else
            {
                result = Graph2(x, bids[bids.Count - 2][1]);
                if (!Is_Possible(result))
                {
                    if (bids[bids.Count - 2][1] == "X" && bids[bids.Count - 1][1] != "X")
                    {
                        result = Find_Longest_Color(x);
                        if (result[0] == bids[bids.Count - 3][1][1])
                            result = Find_Longest_Color(x, true, bids[bids.Count - 3][1][1].ToString());
                        else
                            result = Find_Longest_Color(x);
                        for (int i = 1; i <= 7; i++)
                            if (Is_Possible(i.ToString() + result))
                            {
                                result = i.ToString() + result;
                                break;
                            }

                    }
                    else
                        result = "X";


                }
            }
            tmp.Add(result);
            bids.Add(tmp);
            course.Text += result + " ";

            if (bids.Count >= 4 && bids[bids.Count - 1][1] == "PAS" && bids[bids.Count - 2][1] == "PAS" && bids[bids.Count - 3][1] == "PAS")
                return true;
            return false;


        }

        string Evaluate()
        {



            return "PAS";
        }



        private void Pas(object sender, RoutedEventArgs e)
        {
            List<string> tmp = new List<string>();
            tmp.Add("S");
            tmp.Add("PAS");
            course.Text += "PAS ";
            bids.Add(tmp);

            if (bids.Count >= 4 && bids[bids.Count - 1][1] == "PAS" && bids[bids.Count - 2][1] == "PAS" && bids[bids.Count - 3][1] == "PAS")
            {
                MessageBox.Show("koniec");
                return;
            }




            if (Computer_Move(MainWindow.W))
            {
                MessageBox.Show("koniec");
                this.Close();
            }
            if (Computer_Move(MainWindow.N))
            {
                MessageBox.Show("koniec");
                this.Close();
            }
            if (Computer_Move(MainWindow.E))
            {
                MessageBox.Show("koniec");
                this.Close();
            }

            int counter = 0;
            for (int i = 0; i < bids.Count; i++) 
            {
                if (bids[i][1] == "PAS")
                    counter++;
                if (i == 3)
                    break;
            }

            if (counter == 4)
            {
                MessageBox.Show("cztery pasy");
                this.Close();
            }
        }







        bool Is_5(Player x)
        {
            if (x.dist.C >= 5 || x.dist.D >= 5 || x.dist.H >= 5 || x.dist.S >= 5)
                return true;
            return false;
        }

        string Find_Longest_Color(Player x, bool major = true, string color = null)
        {
            Point C = new Point(x.dist.C, 0);
            Point D = new Point(x.dist.D, 1);
            Point H = new Point(x.dist.H, 2);
            Point S = new Point(x.dist.S, 3);


            List<Point> tmp = new List<Point>() { C, D, H, S };

            if (color != null)
            {
                if (color == "C")
                    tmp.Remove(C);
                if (color == "D")
                    tmp.Remove(D);
                if (color == "H")
                    tmp.Remove(H);
                if (color == "S")
                    tmp.Remove(S);
            }


            if (major == false)
            {
                tmp.Remove(H);
                tmp.Remove(S);
            }
            tmp.Sort(Compare_colors);

            string result = tmp[0].Y.ToString();
            if (result == "0")
                result = "C";
            if (result == "1")
                result = "D";
            if (result == "2")
                result = "H";
            if (result == "3")
                result = "S";

            return result;

        }

        public static int Compare_colors(Point l, Point r)
        {
            if (l.X < r.X)
            {
                return 1;
            }
            else if (l.X > r.X)
                return -1;
            else
            {
                if (l.Y < r.Y)
                    return 1;
                else
                    return -1;
            }
        }

        private void Double(object sender, RoutedEventArgs e)
        {

            bool can_double = false;
            for(int i = 0; i < buttons.Count; i++)
            {
                if (bids[bids.Count - 1][1] == buttons[i].Content.ToString())
                    can_double = true;
            }

            if (bids.Count >= 3)
            {
                if (can_double == false)
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (bids[bids.Count - 3][1] == buttons[i].Content.ToString())
                        {
                            if (bids[bids.Count - 2][1] == "PAS" && bids[bids.Count - 1][1] == "PAS")
                                can_double = true;
                        }
                    }
                }
            }

            if (can_double == false)
                MessageBox.Show("nie można");
            else
                Button_Click(sender, e);


        }
    }
}

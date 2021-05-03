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
            course.Text += "W\tN\tE\tS\n";
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
                course.Text += "*\t";
                Computer_Move(MainWindow.N);
                Computer_Move(MainWindow.E);
            }
            else if (start == 2)
            {
                vulnerability.Text += "\nDealer\tE";
                course.Text += "*\t*\t";
                Computer_Move(MainWindow.E);
            }
            else
            {
                vulnerability.Text += "\nDealer\tS";
                course.Text += "*\t*\t*\t";
            }

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

            if (sender == X_Double) 
            {
                List<string> tmp = new List<string>();
                tmp.Add("S");
                tmp.Add("X");
                course.Text += "X\n";
                bids.Add(tmp);
            }
            else if (sender == Pass)
            {
                List<string> tmp = new List<string>();
                tmp.Add("S");
                tmp.Add("PAS");
                course.Text += "PAS\n";
                bids.Add(tmp);
                if (bids.Count >= 4 && bids[bids.Count - 1][1] == "PAS" && bids[bids.Count - 2][1] == "PAS" && bids[bids.Count - 3][1] == "PAS")
                {
                    MessageBox.Show("koniec");
                    return;
                }
            }
            else
            {
                //zablokowanie niższych odzywek
                for (int i = 0; i <= counter; i++)
                    buttons[i].IsEnabled = false;

                //rejestrowanie przebiegu rozgrywki
                if (Is_Graph1())
                    Graph1(MainWindow.S);
                else if (Is_Graph2())
                    Graph2(MainWindow.S, bids[bids.Count - 2][1]);

                course.Text += buttons[counter].Content + "\n";
                List<string> tmp = new List<string>();
                tmp.Add("S");
                tmp.Add(buttons[counter].Content.ToString());
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
        }

        bool Is_Possible(string bid)
        {
            if (bid == "PAS" || bid == "X")
                return true;

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Content.ToString() == bid)
                {
                    buttons[i].IsEnabled = false;
                    break;
                }
                buttons[i].IsEnabled = false;
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
            if (x.dist.HP < 6)
            {
                x.inf.HP_max = 5;
                return "PAS";
            }

            if (x.dist.HP < 12)
            {
                string color = Find_Longest_Color(x);
                if (color == "C" && x.dist.C >= 6)
                {
                    x.inf.HP_min = 6;
                    x.inf.D_max = 5;
                    x.inf.H_max = 5;
                    x.inf.S_max = 5;
                    return "2C";
                }
                if (color == "D" && x.dist.D >= 6)
                {
                    x.inf.HP_min = 6;
                    x.inf.C_max = 6;
                    x.inf.H_max = 5;
                    x.inf.S_max = 5;
                    return "2D";
                }
                if (color == "H" && x.dist.H >= 6)
                {
                    x.inf.HP_min = 6;
                    x.inf.C_max = 6;
                    x.inf.D_max = 6;
                    x.inf.S_max = 5;
                    return "2H";
                }
                if (color == "S" && x.dist.S >= 6)
                {
                    x.inf.HP_min = 6;
                    x.inf.C_max = 6;
                    x.inf.D_max = 6;
                    x.inf.H_max = 6;
                    return "2S";
                }

                x.inf.HP_max = 11;
                return "PAS";
            }

            x.inf.HP_min = 12;
            if (x.dist.H >= 5 || x.dist.S >= 5)
            {
                string tmpp = "";
                if (x.dist.H >= 5 && x.dist.S < 5)
                {
                    tmpp = "H";
                    x.inf.H_min = 5;
                    x.inf.S_max = 4;
                    x.inf.C_max = 8;
                    x.inf.D_max = 8;
                }
                if (x.dist.S >= 5)
                {
                    tmpp = "S";
                    x.inf.S_min = 5;
                    x.inf.C_max = 8;
                    x.inf.D_max = 8;
                }
                return "1" + tmpp;
            }
            else if (Is_2(x))
            {
                if (x.dist.HP >= 15 && x.dist.HP <= 17)
                {
                    x.inf.H_max = 4;
                    x.inf.S_max = 4;
                    x.inf.C_min = 2;
                    x.inf.D_min = 2;
                    x.inf.H_min = 2;
                    x.inf.S_min = 2;
                    x.inf.HP_min = 15;
                    x.inf.HP_max = 17;
                    return "1BA";
                }
                if (x.dist.HP >= 21 && x.dist.HP <= 24)
                {
                    x.inf.H_max = 4;
                    x.inf.S_max = 4;
                    x.inf.C_min = 2;
                    x.inf.D_min = 2;
                    x.inf.H_min = 2;
                    x.inf.S_min = 2;
                    x.inf.HP_min = 21;
                    x.inf.HP_max = 24;
                    return "2BA";
                }
                if (x.dist.HP >= 25)
                {
                    x.inf.H_max = 4;
                    x.inf.S_max = 4;
                    x.inf.C_min = 2;
                    x.inf.D_min = 2;
                    x.inf.H_min = 2;
                    x.inf.S_min = 2;
                    x.inf.HP_min = 25;
                    return "3BA";
                }
            }

            x.inf.H_max = 4;
            x.inf.S_max = 4;
            string tmp = Find_Longest_Color(x, false);
            if (tmp == "C")
                x.inf.C_min = 3;
            else
                x.inf.D_min = 3;
            return "1" + tmp;
        }

        string Graph2(Player x, string bid)
        {
            string result = "PAS";
            if (bid != "X" && bid[1] == 'B') 
            {
                int height = bid[0];
                height++;

                if (x.dist.H >= 5)
                {
                    x.inf.H_min = 5;
                    result = height + "D";
                }
                else if (x.dist.S >= 5)
                {
                    x.inf.S_min = 5;
                    result = height + "H";
                }
                if (bid == "1BA")
                {


                    if (x.dist.HP <= 7)
                    {
                        x.inf.HP_max = 7;
                        result = "PAS";
                    }
                    else if (x.dist.HP <= 10)
                    {
                        x.inf.HP_min = 8;
                        x.inf.HP_max = 10;
                        result = "2BA";
                    }
                    else if (x.dist.HP <= 14)
                    {
                        x.inf.HP_min = 11;
                        x.inf.HP_max = 14;
                        result = "3BA";
                    }
                    else if (x.dist.HP <= 17)
                    {
                        x.inf.HP_min = 15;
                        x.inf.HP_max = 17;
                        result = "4BA";
                    }
                    else if (x.dist.HP <= 20)
                    {
                        x.inf.HP_min = 18;
                        x.inf.HP_max = 20;
                        result = "6BA";
                    }
                    else
                    {
                        x.inf.HP_min = 21;
                        result = "7BA";
                    }
                }
                else if (bid == "2BA")
                {
                    if (x.dist.HP <= 3)
                    {
                        x.inf.HP_max = 3;
                        result = "PAS";
                    }
                    else if (x.dist.HP <= 8)
                    {
                        x.inf.HP_min = 4;
                        x.inf.HP_max = 8;
                        result = "3BA";
                    }
                    else if (x.dist.HP <= 10)
                    {
                        x.inf.HP_min = 9;
                        x.inf.HP_max = 10;
                        result = "4BA";
                    }
                    else if (x.dist.HP <= 13)
                    {
                        x.inf.HP_min = 11;
                        x.inf.HP_max = 13;
                        result = "6BA";
                    }
                    else
                    {
                        x.inf.HP_min = 13;
                        result = "7BA";
                    }
                }
                else if (bid == "3BA")
                {

                    if (x.dist.HP <= 4)
                    {
                        x.inf.HP_max = 4;
                        result = "PAS";
                    }
                    else if (x.dist.HP <= 7)
                    {
                        x.inf.HP_min = 5;
                        x.inf.HP_max = 7;
                        result = "4BA";
                    }
                    else if (x.dist.HP <= 10)
                    {
                        x.inf.HP_min = 8;
                        x.inf.HP_max = 10;
                        result = "6BA";
                    }
                    else
                    {
                        x.inf.HP_min = 11;
                        result = "7BA";
                    }
                }

                return result;
            }


            int points = x.dist.HP;
            int counter = 0;
            if (points < 6)
                return "PAS";
            if (points < 10)
            {
                x.inf.HP_min = 6;
                x.inf.HP_max = 9;
                counter = 1;
            }
            else if (points < 13)
            {
                x.inf.HP_min = 10;
                x.inf.HP_max = 12;
                counter = 2;
            }
            else if (points < 18)
            {
                x.inf.HP_min = 13;
                x.inf.HP_max = 17;
                counter = 3;
            }
            else
            {
                x.inf.HP_min = 18;
                counter = 4;
            }

            if (bid[0] == 'X')
            {
                string tmp = Find_Longest_Color(x);
                if (tmp == "C")
                    x.inf.C_min = 4;
                if (tmp == "D")
                    x.inf.D_min = 4;
                if (tmp == "H")
                    x.inf.H_min = 4;
                if (tmp == "S")
                    x.inf.S_min = 4;

                result = counter + Find_Longest_Color(x);
                while (!Is_Possible(result))
                    result = counter++ + Find_Longest_Color(x);

                return result;
            }

            if (bid[0] == '2')
            {
                if (points < 13)
                    return "PAS";
                else
                {
                    x.inf.HP_min = 13;
                    result = "2BA";
                    if (!Is_Possible(result))
                    {
                        x.inf.Clear();
                        return "PAS";
                    }
                }
            }
            else
            {
                if (bid[1] == 'S')
                {
                    if (x.dist.S >= 3)
                    {
                        x.inf.S_min = 3;
                        result = (counter + 1).ToString() + "S";
                        if (!Is_Possible(result))
                        {
                            x.inf.Clear();
                            return "PAS";
                        }
                    }
                    else
                    {
                        if (Is_5(x))
                        {
                            string tmp = Find_Longest_Color(x);
                            x.inf.S_max = 2;
                            if (tmp == "C")
                            {
                                x.inf.C_min = 5;
                                x.inf.D_max = 4;
                                x.inf.H_max = 4;
                            }
                            else if (tmp == "D")
                            {
                                x.inf.D_min = 5;
                                x.inf.H_max = 4;
                            }
                            else
                                x.inf.H_min = 5;
                            result = counter + Find_Longest_Color(x);
                        }
                        if (Is_Possible(result))
                            return result;
                        else
                        {
                            int tmp_min = x.inf.HP_min;
                            int tmp_max = x.inf.HP_max;
                            x.inf.Clear();
                            x.inf.HP_min = tmp_min;
                            x.inf.HP_max = tmp_max;
                            x.inf.C_max = 4;
                            x.inf.D_max = 4;
                            x.inf.H_max = 4;
                            x.inf.S_max = 2;
                            result = counter + "BA";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                    }
                }


                else if (bid[1] == 'H')
                {
                    if (x.dist.H >= 3)
                    {
                        x.inf.H_min = 3;
                        result = (counter + 1).ToString() + "H";
                        if (!Is_Possible(result))
                        {
                            x.inf.Clear();
                            return "PAS";
                        }
                    }
                    else
                    {
                        if (Is_5(x))
                        {
                            string tmp = Find_Longest_Color(x);
                            x.inf.H_max = 2;
                            if (tmp == "C")
                            {
                                x.inf.C_min = 5;
                                x.inf.D_max = 4;
                                x.inf.S_max = 4;
                            }
                            else if (tmp == "D")
                            {
                                x.inf.D_min = 5;
                                x.inf.S_max = 4;
                            }
                            else
                                x.inf.S_min = 5;
                            result = counter + Find_Longest_Color(x);
                        }
                        if (Is_Possible(result))
                            return result;
                        else
                        {
                            int tmp_min = x.inf.HP_min;
                            int tmp_max = x.inf.HP_max;
                            x.inf.Clear();
                            x.inf.HP_min = tmp_min;
                            x.inf.HP_max = tmp_max;
                            x.inf.C_max = 4;
                            x.inf.D_max = 4;
                            x.inf.H_max = 2;
                            x.inf.S_max = 4;
                            result = counter + "BA";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                    }
                }

                else if (bid[1] == 'D')
                {
                    if (Is_5(x) && Find_Longest_Color(x) != "D")
                    {
                        string tmp = Find_Longest_Color(x);
                        if (tmp == "C")
                        {
                            x.inf.C_min = 5;
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                        }
                        else if (tmp == "H")
                        {
                            x.inf.H_min = 5;
                            x.inf.S_max = 4;
                        }
                        else
                            x.inf.S_min = 5;
                        result = counter + Find_Longest_Color(x);
                    }

                    if (Is_Possible(result) && result != "PAS") 
                        return result;
                    else
                    {
                        int tmp_min = x.inf.HP_min;
                        int tmp_max = x.inf.HP_max;
                        x.inf.Clear();
                        x.inf.HP_min = tmp_min;
                        x.inf.HP_max = tmp_max;

                        if (x.dist.D >= 4)
                        {
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                            x.inf.D_min = 4;
                            result = (counter + 1).ToString() + "D";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                        else
                        {
                            x.inf.C_max = 4;
                            x.inf.D_max = 3;
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                            result = counter + "BA";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                    }
                }


                else if (bid[1] == 'C')
                {
                    if (Is_5(x) && Find_Longest_Color(x) != "C")
                    {
                        string tmp = Find_Longest_Color(x);
                        if (tmp == "D")
                        {
                            x.inf.D_min = 5;
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                        }
                        else if (tmp == "H")
                        {
                            x.inf.H_min = 5;
                            x.inf.S_max = 4;
                        }
                        else
                            x.inf.S_min = 5;
                        result = counter + Find_Longest_Color(x);
                    }

                    if (Is_Possible(result))
                        return result;
                    else
                    {
                        int tmp_min = x.inf.HP_min;
                        int tmp_max = x.inf.HP_max;
                        x.inf.Clear();
                        x.inf.HP_min = tmp_min;
                        x.inf.HP_max = tmp_max;

                        if (x.dist.C >= 4)
                        {
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                            x.inf.D_max = 4;
                            x.inf.C_min = 4;
                            result = (counter + 1).ToString() + "C";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                        else
                        {
                            x.inf.C_max = 3;
                            x.inf.D_max = 4;
                            x.inf.H_max = 4;
                            x.inf.S_max = 4;
                            result = counter + "BA";
                            if (!Is_Possible(result))
                            {
                                x.inf.Clear();
                                return "PAS";
                            }
                        }
                    }
                }
            
            }  
            return result;
        }

        bool Is_Graph1()
        {
            if (bids.Count < 2)
                return true;
            else if (bids.Count == 2)
            {
                if (bids[0][1] == "PAS")
                    return true;
                else
                    return false;
            }
            else if(bids.Count == 3)
            {
                if (bids[1][1] == "PAS")
                    return true;
                else
                    return false;
            }
            return false;
        }

        bool Is_Graph2()
        {
            if (bids.Count == 2)
            {
                if (bids[0][1] != "PAS")
                    return true;
                else
                    return false;
            }
            else if (bids.Count == 3)
            {
                if (bids[1][1] != "PAS")
                    return true;
                else
                    return false;
            }
            else if (bids.Count == 4)
            {
                if (bids[0][1] == "PAS" && bids[2][1] != "PAS")
                    return true;
                else
                    return false;
            }
            else if (bids.Count == 5)
            {
                if (bids[1][1] == "PAS" && bids[3][1] != "PAS")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        bool Computer_Move(Player x)
        {
            List<string> tmp = new List<string>();
            tmp.Add(x.index);
            string result = "";

            if (Is_Graph1())
            {
                result = Graph1(x);
                if (!Is_Possible(result))
                    result = "X";
            }
            else if (Is_Graph2())
            {
                result = Graph2(x, bids[bids.Count - 2][1]);
                if (!Is_Possible(result))
                {
                    x.inf.Clear();
                    if (bids[bids.Count - 1][1] == "X")
                    {
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
            else
                result = Evaluate(x);

            if (!Is_Possible(result))
                result = "PAS";

            tmp.Add(result);
            bids.Add(tmp);
            course.Text += result + "\t";

            if (bids.Count >= 4 && bids[bids.Count - 1][1] == "PAS" && bids[bids.Count - 2][1] == "PAS" && bids[bids.Count - 3][1] == "PAS")
                return true;
            return false;


        }

        string Evaluate(Player x)
        {
            Player y = new Player();
            if (x.index == "E")
                y = MainWindow.W;
            if (x.index == "W")
                y = MainWindow.E;
            if (x.index == "N")
                y = MainWindow.S;
            if (x.index == "S")
                y = MainWindow.N;

            Information tmp = new Information();
            tmp.HP_min = x.dist.HP + y.inf.HP_min;
            tmp.HP_max = x.dist.HP + y.inf.HP_max;
            tmp.C_min = x.dist.C + y.inf.C_min;
            tmp.C_max = x.dist.C + y.inf.C_max;
            tmp.D_min = x.dist.D + y.inf.D_min;
            tmp.D_max = x.dist.D + y.inf.D_max;
            tmp.H_min = x.dist.H + y.inf.H_min;
            tmp.H_max = x.dist.H + y.inf.H_max;
            tmp.S_min = x.dist.S + y.inf.S_min;
            tmp.S_max = x.dist.S + y.inf.S_max;

            if (tmp.HP_min < 25)
                return "PAS";
            else if (tmp.HP_min < 31)
            {
                if (tmp.H_min >= 8)
                    return "4H";
                if (tmp.S_min >= 8)
                    return "4S";
                if (tmp.D_min >= 8 && tmp.HP_min >= 28)
                    return "5D";
                if (tmp.C_min >= 8 && tmp.HP_min >= 28)
                    return "5C";
                return "3BA";
            }
            else
                return "4BA";
        }

        bool Is_2(Player x)
        {
            if (x.dist.C >= 2 && x.dist.D >= 2 && x.dist.H >= 2 && x.dist.S >= 2) 
                return true;
            return false;
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace bridge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        static public Player N = new Player();
        static public Player E = new Player();
        static public Player S = new Player();
        static public Player W = new Player();
        static public List<Player> players = new List<Player>();

        List<Card> deck = new List<Card>();
        List<BitmapImage> bitmapImages = new List<BitmapImage>();
        
        
        public MainWindow()
        {


            InitializeComponent();

            players.Add(N);
            players.Add(E);
            players.Add(S);
            players.Add(W);


            //dodawanie obrazkow
            for (int i = 1, a = 0, b = 0; i <= 52; i++)
            {
                string path;
                Card tmp = new Card();
                //zmiana ścieżki
                path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\";
                a = i % 10;
                b = (i - a) / 10;
                path += (char)(b + 48);
                path += (char)(a + 48);
                path += ".png";

                //dodanie obrazka

                Uri uri = new Uri(path);
                var bitmap = new BitmapImage(uri);
                tmp.image = bitmap;

                path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\";
                path += "rewers.png";
                uri = new Uri(path);
                bitmap = new BitmapImage(uri);

                tmp.reverse_image = bitmap;
                tmp.value = (i - 1) % 13;
                tmp.Set_HP();
                tmp.trump = false;
                tmp.Set_Color(i - 1);
                deck.Add(tmp);
            }


            //rozdanie kart
            Random random = new Random();
            int r;

            for (int i = 0; i < 13; i++)
            {
                r = random.Next(deck.Count);
                Card tmp = new Card();
                tmp = deck[r];
                N.cards.Add(tmp);
                deck.Remove(tmp);
            }

            for (int i = 0; i < 13; i++)
            {
                r = random.Next(deck.Count);
                Card tmp = new Card();
                tmp = deck[r];
                E.cards.Add(tmp);
                deck.Remove(tmp);
            }

            for (int i = 0; i < 13; i++)
            {
                r = random.Next(deck.Count);
                Card tmp = new Card();
                tmp = deck[r];
                S.cards.Add(tmp);
                deck.Remove(tmp);
            }

            for (int i = 0; i < 13; i++)
            {
                r = random.Next(deck.Count);
                Card tmp = new Card();
                tmp = deck[r];
                W.cards.Add(tmp);
                deck.Remove(tmp);
            }

            N.cardplace.Add(N01);
            N.cardplace.Add(N02);
            N.cardplace.Add(N03);
            N.cardplace.Add(N04);
            N.cardplace.Add(N05);
            N.cardplace.Add(N06);
            N.cardplace.Add(N07);
            N.cardplace.Add(N08);
            N.cardplace.Add(N09);
            N.cardplace.Add(N10);
            N.cardplace.Add(N11);
            N.cardplace.Add(N12);
            N.cardplace.Add(N13);

            E.cardplace.Add(E01);
            E.cardplace.Add(E02);
            E.cardplace.Add(E03);
            E.cardplace.Add(E04);
            E.cardplace.Add(E05);
            E.cardplace.Add(E06);
            E.cardplace.Add(E07);
            E.cardplace.Add(E08);
            E.cardplace.Add(E09);
            E.cardplace.Add(E10);
            E.cardplace.Add(E11);
            E.cardplace.Add(E12);
            E.cardplace.Add(E13);

            S.cardplace.Add(S01);
            S.cardplace.Add(S02);
            S.cardplace.Add(S03);
            S.cardplace.Add(S04);
            S.cardplace.Add(S05);
            S.cardplace.Add(S06);
            S.cardplace.Add(S07);
            S.cardplace.Add(S08);
            S.cardplace.Add(S09);
            S.cardplace.Add(S10);
            S.cardplace.Add(S11);
            S.cardplace.Add(S12);
            S.cardplace.Add(S13);

            W.cardplace.Add(W01);
            W.cardplace.Add(W02);
            W.cardplace.Add(W03);
            W.cardplace.Add(W04);
            W.cardplace.Add(W05);
            W.cardplace.Add(W06);
            W.cardplace.Add(W07);
            W.cardplace.Add(W08);
            W.cardplace.Add(W09);
            W.cardplace.Add(W10);
            W.cardplace.Add(W11);
            W.cardplace.Add(W12);
            W.cardplace.Add(W13);


            for (int i = 0; i < 13; i++)
            {
                S.cardplace[i].Source = S.cards[i].image;
                W.cardplace[i].Source = W.cards[i].image;
                N.cardplace[i].Source = N.cards[i].image;
                E.cardplace[i].Source = E.cards[i].image;
            }

            N.index = "N";
            E.index = "E";
            S.index = "S";
            W.index = "W";

        }

        private void Arrange(object sender, RoutedEventArgs e)
        {

           
            N.Rearrange();
            E.Rearrange();
            S.Rearrange();
            W.Rearrange();

            N.Set_Hand();
            E.Set_Hand();
            S.Set_Hand();
            W.Set_Hand();

            LN.Content = N.dist.HP + "pkt";
            LE.Content = E.dist.HP + "pkt";
            LS.Content = S.dist.HP + "pkt";
            LW.Content = W.dist.HP + "pkt";
        }

        private void Hide(object sender, RoutedEventArgs e)
        {     
            N.Hide();
            E.Hide();
            W.Hide();
        }

        private void Bidding(object sender, RoutedEventArgs e)
        {
            Bid window = new Bid();
            window.Show();
        }
    }
}
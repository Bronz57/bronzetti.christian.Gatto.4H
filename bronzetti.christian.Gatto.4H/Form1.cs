using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using bronzetti.christian.Gatto._4H.Models;

namespace bronzetti.christian.Gatto._4H
{
    public partial class frmMain : Form
    {
        // foglio di quaderno 20 cm x 30 cm
        const int LATO_X = 200;
        const int LATO_Y = 300;

        const int MARGINE_SINISTRO = 100;
        const int MARGINE_DESTRO = 100;
        const int MARGINE_SUPERIORE = 100;
        const int MARGINE_INFERIORE = 100;


        const int LATO_X_FORM = (MARGINE_SINISTRO + LATO_X + MARGINE_DESTRO);
        const int LATO_Y_FORM = (MARGINE_SUPERIORE + LATO_Y + MARGINE_INFERIORE);


        public frmMain()
        {
            InitializeComponent();

        }

        static int CalcolaRigheFile()
        {
            int contaRighe = -1;
            using (StreamReader sr = new StreamReader(@"Punti.csv"))
            {
                while (sr.ReadLine() != null)
                    contaRighe++;
            }

                return contaRighe;
        }
        private int CalcolaXForm(double x)
        {
            int xLogico = 0;
            xLogico = (int)(x) + MARGINE_SINISTRO;
            return xLogico;
        }

        private int CalcolaYForm(double y)
        {
            int yLogico = 0;
            yLogico = (MARGINE_SUPERIORE + LATO_Y - (int)(y));
            return yLogico;
        }
        private void Disegna(Punto[] p, Pen b, Graphics dc)
        {
            int i;
            for (i = 0; i < p.Length; i++)
            {
                int j = i + 1;
                if (j == p.Length) j = 0;
                    dc.DrawLine(b, CalcolaXForm(p[i].X), CalcolaYForm(p[i].Y),  //b è la penna
                                CalcolaXForm(p[j].X), CalcolaYForm(p[j].Y)); //disegna un segmento tra i 2 punti con penna scelta, 
            }
        }

        private string[] CaricaVettore(string p, string []pG) //carico il vettore 
        {
            StreamReader sr = new StreamReader(p);
            for (int i = 0; i < pG.Length; i++)
                pG[i] = sr.ReadLine(); //a ogni "casella" del vettore corrisponte la coordinata 

            sr.Close(); //chiudo il csv

            return pG;
        }
        
        private int ContaGrandezzaVettore(string[]pG, int k) //metodo che conta quanto è lungo il vettore
        {
            int i = k; //k indica la riga del csv

            
            //gira fino anche trova space cioè il divisore 
            while (pG[i] != "space")
            {
                if (i == CalcolaRigheFile())
                    return i;
                    
                i++;
            }
                

            return i;
        }

        private void TrovaPartiFigura(string[] pG, int k, int j, int i, Pen nera, Graphics dc)
        {
            int n = 0; //variabile di appoggio per copiare k  non incrementato
            while(k!=CalcolaRigheFile()) //gira per tutta la lunghezza del vettore
            {
                
               Punto[] pezzoFigura = new Punto[ContaGrandezzaVettore(pG, k) - n - j]; //crea dei mini vettori ad ogni giro
                
                for (i = 0; i < pezzoFigura.Length; i++)
                {
                    pezzoFigura[i] = new Punto(pG[k]); //assegno per ogni indice il valore della coordinata corrispondente
                    k++; //k indica l'indice del vettore con tutte le coordinate
                    n = k; //k non incrementato 
                }
                
                
                k += 1; //k indica dove ci troviamo nelle righe del csv il +1 indica il salto del divisore space
                j = 1;
                
                Disegna(pezzoFigura, nera, dc); //chiamo il metodo ad ogni giro del codice passando i punti corrispondenti, penne e grafica
            }

            
            
        }
        private void btnCalcola_Click(object sender, EventArgs e)
        {
            
            Graphics dc = this.CreateGraphics();
            Pen BluePen = new Pen(Color.Blue, 1);
            Pen RedPen = new Pen(Color.Red, 1);
            Pen BlackPen = new Pen(Color.Black, 2);


            dc.DrawRectangle(BluePen, MARGINE_SINISTRO + 0, MARGINE_SUPERIORE + 0, LATO_X, LATO_Y);

            string percorsoFile = @"Punti.csv"; //Metto in string il nome del file per poi usarlo
            
            string [] puntiGatto = new string[38]; //38 righe del csv

            //prendo dati dal csv e li passo ad un vettore
            CaricaVettore(percorsoFile, puntiGatto); //metodo che carica il vettore

            int k = 0; //contatore e indice del vettore con tutti i punti
            int i = 0; //contatore e indice del vettore analizzato
            int j = 0;
            
            TrovaPartiFigura(puntiGatto, k, j, i, BlackPen, dc); //chiama il metodo che mi scompone la figura in varie parti:
                                                                //testa, corpo, coda, zampe Anteriori e 
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using Administrador_Service_Desk.Modelo;


namespace Administrador_Service_Desk
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // BOTON PARA TESTEAR
            pcat pcat = new pcat();

            MessageBox.Show(pcat.utilUno());
            

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // EXPORTAR A UN EXCEL
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            excel = new Microsoft.Office.Interop.Excel.Application();

            // HACER QUE EL EXCEL SEA VISIBLE
            excel.Visible = false;
            excel.DisplayAlerts = false;

            // CREANDO UN NUEVO LIBRO
            excelworkBook = excel.Workbooks.Add(Type.Missing);

            // USAR LA HOJA
            excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "Categorías";


            //excelSheet.Cells[1, 1] = "SONDIX";
            //excelSheet.Cells[1, 2] = "Fecha de Descarga : " + DateTime.Now.ToShortDateString();

            // AUTO RESIZE LAS CELDAS
            excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[dataGridView1.Rows.Count, dataGridView1.Columns.Count]];
            excelCellrange.EntireColumn.AutoFit();
            // BORDECITOS
            Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;

            // Escribir en las celdas X=2(FILA)   Y=2 (COLUMNA)

            int fila = dataGridView1.Rows.Count;
            int colu = dataGridView1.Columns.Count;
            


            for (int x = 0; x < fila; x++)
            {
                for (int y = 0; y < colu; y++)
                {
                    excelSheet.Cells[(x + 2), (y + 1)] = dataGridView1.Rows[x].Cells[y].Value;
                }
            }

            excel.Visible = true;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            pcat cat = new pcat();

            List<pcat> lista = cat.listaCategorias();

            //label1.Text = lista[0].Sym;

            if (lista != null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("#");
                dt.Columns.Add("id");
                dt.Columns.Add("persistent_id");
                dt.Columns.Add("sym");
                dt.Columns.Add("del");
                dt.Columns.Add("group_id");
                dt.Columns.Add("service_type");
                dt.Columns.Add("cr_flag");
                dt.Columns.Add("in_flag");
                dt.Columns.Add("pr_flag");
                dt.Columns.Add("ss_include");
                dt.Columns.Add("ss_sym");
                dt.Columns.Add("tenant");

                int contador = 1;

                foreach (pcat c in lista)
                {
                    dt.Rows.Add(contador,
                        c.Id,
                        c.Persistent_id,
                        c.Sym, c.Del,
                        c.Group_id,
                        c.Service_type,
                        c.Cr_flag,
                        c.In_flag,
                        c.Pr_flag,
                        c.Ss_include,
                        c.Ss_sym,
                        c.Tenant);

                    contador++;
                }

                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Lista de categorías esta vacío...");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float cantidad = 1581;
            float num = cantidad / 250;
            float num2 = 6.4F + num;

            MessageBox.Show(num.ToString());
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void categoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // CATEGORÍAS
        }
    }
}

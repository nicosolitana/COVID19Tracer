//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

using SerialSimulation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;

namespace SerialSimulation.Controllers
{
    class DataLoading
    {

        private static OleDbConnection OpenConnection(string path)
        {
            OleDbConnection oledbConn = null;
            try
            {
                if (Path.GetExtension(path) == ".xls")
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + path + "; Extended Properties= \"Excel 8.0;HDR=Yes;IMEX=1\"");
                else if (Path.GetExtension(path) == ".xlsx")
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + "; Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                oledbConn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return oledbConn;
        }

        private static IList<TracerData> ExtractEmployeeExcel(OleDbConnection oledbConn)
        {
            List<TracerData> tdData = new List<TracerData>();
            for (int i = 1; i <= 12; i++)
            {
                string sheetName = "Set" + i.ToString();
                string query = sheetName + "$";
                OleDbCommand cmd = new OleDbCommand(); ;
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet dsPeepsInfo = new DataSet();

                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM ["+ query + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(dsPeepsInfo, sheetName);

                var dsPeepsInfoLst = dsPeepsInfo.Tables[0].AsEnumerable().Select(s => new TracerData
                {
                    Name = new Person()
                    {
                        firstName = Convert.ToString(s["FirstName"] != DBNull.Value ? s["FirstName"] : "").Trim(),
                        lastName = Convert.ToString(s["LastName"] != DBNull.Value ? s["LastName"] : "").Trim(),
                        address = Convert.ToString(s["Address"] != DBNull.Value ? s["Address"] : "")
                    },
                    History = new Activities()
                    {
                        dateData = Convert.ToString(s["Date"]).Remove(10),
                        timeData = DateTime.Parse(Convert.ToString(s["Hour"]), CultureInfo.CurrentCulture).ToString("HH:mm:ss tt"),
                        Location = Convert.ToString(s["Place"] != DBNull.Value ? s["Place"] : "")
                    }
                }).ToList<TracerData>();
                tdData.AddRange(dsPeepsInfoLst);
            }
            //return dsPeepsInfoLst;
            return tdData;
        }

        private static IList<TracerData> ReadExcel(string path)
        {
            IList<TracerData> objEmployeeInfo = new List<TracerData>();
            try
            {
                OleDbConnection oledbConn = OpenConnection(path);
                if (oledbConn.State == ConnectionState.Open)
                {
                    objEmployeeInfo = ExtractEmployeeExcel(oledbConn);
                    oledbConn.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return objEmployeeInfo;
        }

        public List<TracerData> LoadData()
        {
            List<TracerData> _tracerData = new List<TracerData>();
            string exePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //_tracerData.AddRange(ReadExcel(exePath + @"\DataFolder\SetOne.xls").ToList());
            //_tracerData.AddRange(ReadExcel(exePath + @"\DataFolder\SetTwo.xls").ToList());
            _tracerData.AddRange(ReadExcel(exePath + @"\DataFolder\dataset.xls").ToList());
            return _tracerData;
        }
    }
}

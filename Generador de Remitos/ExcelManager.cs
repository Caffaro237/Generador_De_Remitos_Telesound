using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Diagnostics;

namespace Generador_de_Remitos
{
    public class ExcelManager
    {
        private XSSFWorkbook workbook;
        private ISheet hoja;

        private string pathCompleto;
        private bool existeArchivo;
        private bool esRemito;

        #region Constructor 

        public ExcelManager(bool esRemitoParam)
        {
            try
            {
                this.esRemito = esRemitoParam;

                if (!Directory.Exists(Utils.PathExcel))
                {
                    Directory.CreateDirectory(Utils.PathExcel);
                }

                if (esRemito)
                {
                    pathCompleto = Utils.PathExcel + Utils.NombreExcelRemito;
                }
                else
                {
                    pathCompleto = Utils.PathExcel + Utils.NombreExcelGarantia;
                }

                existeArchivo = File.Exists(pathCompleto);

                workbook = existeArchivo ? new XSSFWorkbook(File.Open(pathCompleto, FileMode.Open)) : new XSSFWorkbook();

                if (esRemito)
                {
                    hoja = workbook.GetSheet("Remito");

                    if (hoja == null)
                    {
                        hoja = workbook.CreateSheet("Remito");
                    }
                }
                else
                {
                    hoja = workbook.GetSheet("Garantia");

                    if (hoja == null)
                    {
                        hoja = workbook.CreateSheet("Garantia");
                    }
                }
                
                hoja.DisplayGridlines = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("used by another process"))
                {
                    if (esRemito)
                    {
                        throw new Exception("Cerrar el archivo antes de clickear Imprimir Remito");
                    }
                    else
                    {
                        throw new Exception("Cerrar el archivo antes de clickear Crear Garantia");
                    }
                }

                throw ex;
            }
        }

        #endregion

        #region Metodos Publicos

        public void GenerarExcel(DataTable dataTable)
        {
            try
            {
                this.combinarCeldas();

                this.setearCeldasCombinadas(dataTable);

                if (!esRemito)
                {
                    this.sumarCeldasGarantia();
                    this.setearCeldasCombinadasGarantia(dataTable);
                }

                this.definirEstilo();

                //Escribe todo lo que se genero previamente en la Hoja de Excel
                using (FileStream stream = new FileStream(pathCompleto, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Metodos Privados

        private void combinarCeldas()
        {
            try
            {
                CellRangeAddress regionCeldas;

                // Combinar las celdas J4 a L5
                regionCeldas = new CellRangeAddress(3, 4, 9, 11);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D8 a M9
                regionCeldas = new CellRangeAddress(7, 8, 3, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D10 a G11
                regionCeldas = new CellRangeAddress(9, 10, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D12 a G13
                regionCeldas = new CellRangeAddress(11, 12, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas J10 a M11
                regionCeldas = new CellRangeAddress(9, 10, 9, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas J12 a M13
                regionCeldas = new CellRangeAddress(11, 12, 9, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D15 a G17
                regionCeldas = new CellRangeAddress(14, 16, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D18 a G20
                regionCeldas = new CellRangeAddress(17, 19, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D21 a G23
                regionCeldas = new CellRangeAddress(20, 22, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la H15 a M19
                regionCeldas = new CellRangeAddress(14, 18, 7, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la H21 a M23
                regionCeldas = new CellRangeAddress(20, 22, 7, 12);
                hoja.AddMergedRegion(regionCeldas);

                if (esRemito)
                {
                    // Combinar celdas de la C26 a I27
                    regionCeldas = new CellRangeAddress(25, 26, 2, 8);
                    hoja.AddMergedRegion(regionCeldas);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void sumarCeldasGarantia()
        {
            try
            {
                int celdasDobles = 30;
                CellRangeAddress regionCeldas;

                // Combinar las celdas J34 a L35
                regionCeldas = new CellRangeAddress(3 + celdasDobles, 4 + celdasDobles, 9, 11);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D37 a M38
                regionCeldas = new CellRangeAddress(7 + celdasDobles, 8 + celdasDobles, 3, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D40 a G41
                regionCeldas = new CellRangeAddress(9 + celdasDobles, 10 + celdasDobles, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas D42 a G43
                regionCeldas = new CellRangeAddress(11 + celdasDobles, 12 + celdasDobles, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas J40 a M41
                regionCeldas = new CellRangeAddress(9 + celdasDobles, 10 + celdasDobles, 9, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar las celdas J42 a M43
                regionCeldas = new CellRangeAddress(11 + celdasDobles, 12 + celdasDobles, 9, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D45 a G47
                regionCeldas = new CellRangeAddress(14 + celdasDobles, 16 + celdasDobles, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D48 a G50
                regionCeldas = new CellRangeAddress(17 + celdasDobles, 19 + celdasDobles, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la D51 a G53
                regionCeldas = new CellRangeAddress(20 + celdasDobles, 22 + celdasDobles, 3, 6);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la H45 a M49
                regionCeldas = new CellRangeAddress(14 + celdasDobles, 18 + celdasDobles, 7, 12);
                hoja.AddMergedRegion(regionCeldas);

                // Combinar celdas de la H51 a M53
                regionCeldas = new CellRangeAddress(20 + celdasDobles, 22 + celdasDobles, 7, 12);
                hoja.AddMergedRegion(regionCeldas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setearCeldasCombinadas(DataTable datos)
        {
            try
            {
                IRow fila;
                ICell celda;

                // Obtener la celda combinada D8:M9
                fila = hoja.GetRow(7);
                if (fila == null)
                {
                    fila = hoja.CreateRow(7);
                }

                // Setea la celda combinada D8:M9 con la fecha del dia
                celda = fila.CreateCell(3);
                celda.SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));



                // Obtener la celda combinada J4:L5
                fila = hoja.GetRow(3);
                if (fila == null)
                {
                    fila = hoja.CreateRow(3);
                }

                // Setea la celda combinada J4:L5 con la orden de service
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["OrdenService"].ToString());



                // Obtener la celda combinada D10:G11
                fila = hoja.GetRow(9);
                if (fila == null)
                {
                    fila = hoja.CreateRow(9);
                }

                // Setea la celda combinada D10:G11 con el nombre y apellido
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["NombreYApellido"].ToString());

                
                
                // Obtener la celda combinada J10:M11
                fila = hoja.GetRow(9);
                if (fila == null)
                {
                    fila = hoja.CreateRow(9);
                }

                // Setea la celda combinada J10:M11 con el Telefono
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["Telefono"].ToString());

                
                
                // Obtener la celda combinada D12:G13
                fila = hoja.GetRow(11);
                if (fila == null)
                {
                    fila = hoja.CreateRow(11);
                }

                // Setea la celda combinada D12:G13 con la Localidad
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Localidad"].ToString());

                
                
                // Obtener la celda combinada J12:M13
                fila = hoja.GetRow(11);
                if (fila == null)
                {
                    fila = hoja.CreateRow(11);
                }

                // Setea la celda combinada J12:M13 con el Domicilio
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["Domicilio"].ToString());

                
                
                // Obtener la celda combinada D15:G17
                fila = hoja.GetRow(14);
                if (fila == null)
                {
                    fila = hoja.CreateRow(14);
                }

                // Setea la celda combinada D15:G17 con el Tipo
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Tipo"].ToString());

                
                
                // Obtener la celda combinada D18:G20
                fila = hoja.GetRow(17);
                if (fila == null)
                {
                    fila = hoja.CreateRow(17);
                }

                // Setea la celda combinada D18:G20 con la Marca
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Marca"].ToString());

                
                
                // Obtener la celda combinada D21:G23
                fila = hoja.GetRow(20);
                if (fila == null)
                {
                    fila = hoja.CreateRow(20);
                }

                // Setea la celda combinada D21:G23 con el Modelo
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Modelo"].ToString());

                
                if (esRemito)
                {
                    // Obtener la celda combinada H15:M19
                    fila = hoja.GetRow(14);
                    if (fila == null)
                    {
                        fila = hoja.CreateRow(14);
                    }

                    // Setea la celda combinada H15:M19 con el Motivo de la Falla
                    celda = fila.CreateCell(7);
                    celda.SetCellValue(datos.Rows[0]["MotivoFalla"].ToString());
                }
                else
                {
                    // Obtener la celda combinada H15:M19
                    fila = hoja.GetRow(14);
                    if (fila == null)
                    {
                        fila = hoja.CreateRow(14);
                    }

                    // Setea la celda combinada H15:M19 con la Reparacion a Efectuar
                    celda = fila.CreateCell(7);
                    celda.SetCellValue(datos.Rows[0]["ReparacionAEfectuar"].ToString());
                }

                if (esRemito)
                {
                    // Obtener la celda combinada H21:M23
                    fila = hoja.GetRow(20);
                    if (fila == null)
                    {
                        fila = hoja.CreateRow(20);
                    }

                    // Setea la celda combinada H21:M23 con los Accesorios
                    celda = fila.CreateCell(7);
                    celda.SetCellValue(datos.Rows[0]["Accesorios"].ToString());
                }
                else
                {
                    // Obtener la celda combinada H21:M23
                    fila = hoja.GetRow(20);
                    if (fila == null)
                    {
                        fila = hoja.CreateRow(20);
                    }

                    // Setea la celda combinada H15:M19 con el Valor de la Reparacion
                    celda = fila.CreateCell(7);
                    celda.SetCellValue(datos.Rows[0]["ValorReparacion"].ToString());
                }


                if (esRemito)
                {
                    // Obtener la celda combinada C26:I27
                    fila = hoja.GetRow(25);
                    if (fila == null)
                    {
                        fila = hoja.CreateRow(25);
                    }

                    // Setea la celda combinada C26:I27 con las Observaciones
                    celda = fila.CreateCell(2);
                    celda.SetCellValue(datos.Rows[0]["Observaciones"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setearCeldasCombinadasGarantia(DataTable datos)
        {
            try
            {
                int celdasDobles = 30;
                IRow fila;
                ICell celda;

                // Obtener la celda combinada D38:M39
                fila = hoja.GetRow(7 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(7 + celdasDobles);
                }

                // Setea la celda combinada D38:M39 con la fecha del dia
                celda = fila.CreateCell(3);
                celda.SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));


                // Obtener la celda combinada J34:L35
                fila = hoja.GetRow(3 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(3 + celdasDobles);
                }

                // Setea la celda combinada J34:L35 con la orden de service
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["OrdenService"].ToString());



                // Obtener la celda combinada D40:G41
                fila = hoja.GetRow(9 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(9 + celdasDobles);
                }

                // Setea la celda combinada D40:G41 con el nombre y apellido
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["NombreYApellido"].ToString());



                // Obtener la celda combinada J40:M41
                fila = hoja.GetRow(9 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(9 + celdasDobles);
                }

                // Setea la celda combinada J40:M41 con el Telefono
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["Telefono"].ToString());



                // Obtener la celda combinada D42:G43
                fila = hoja.GetRow(11 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(11 + celdasDobles);
                }

                // Setea la celda combinada D42:G43 con la Localidad
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Localidad"].ToString());



                // Obtener la celda combinada J42:M43
                fila = hoja.GetRow(11 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(11 + celdasDobles);
                }

                // Setea la celda combinada J42:M43 con el Domicilio
                celda = fila.CreateCell(9);
                celda.SetCellValue(datos.Rows[0]["Domicilio"].ToString());



                // Obtener la celda combinada D45:G47
                fila = hoja.GetRow(14 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(14 + celdasDobles);
                }

                // Setea la celda combinada D45:G47 con el Tipo
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Tipo"].ToString());



                // Obtener la celda combinada D48:G50
                fila = hoja.GetRow(17 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(17 + celdasDobles);
                }

                // Setea la celda combinada D48:G50 con la Marca
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Marca"].ToString());



                // Obtener la celda combinada D51:G53
                fila = hoja.GetRow(20 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(20 + celdasDobles);
                }

                // Setea la celda combinada D51:G53 con el Modelo
                celda = fila.CreateCell(3);
                celda.SetCellValue(datos.Rows[0]["Modelo"].ToString());


                // Obtener la celda combinada H45:M49
                fila = hoja.GetRow(14 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(14 + celdasDobles);
                }

                // Setea la celda combinada H45:M49 con la Reparacion a Efectuar
                celda = fila.CreateCell(7);
                celda.SetCellValue(datos.Rows[0]["ReparacionAEfectuar"].ToString());


                // Obtener la celda combinada H51:M53
                fila = hoja.GetRow(20 + celdasDobles);
                if (fila == null)
                {
                    fila = hoja.CreateRow(20 + celdasDobles);
                }

                // Setea la celda combinada H51:M53 con el Valor de la Reparacion
                celda = fila.CreateCell(7);
                celda.SetCellValue(datos.Rows[0]["ValorReparacion"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void definirEstilo()
        {
            try
            {
                // Definir el estilo que deseas aplicar a todas las celdas
                ICellStyle style = workbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.WrapText = true;

                // Iterar sobre todas las filas y celdas en la hoja y aplicar el estilo a cada celda
                foreach (IRow filaStyle in hoja)
                {
                    foreach (ICell celdaStyle in filaStyle)
                    {
                        celdaStyle.CellStyle = style;
                    }
                }


                // Ajustar los márgenes de la página
                hoja.SetMargin(MarginType.LeftMargin, 0.25);
                hoja.SetMargin(MarginType.RightMargin, 0.25);
                hoja.SetMargin(MarginType.TopMargin, 0.25);
                hoja.SetMargin(MarginType.BottomMargin, 0.25);

                // Ajustar la hoja en una página
                hoja.FitToPage = true;
                hoja.PrintSetup.FitWidth = 1;
                hoja.PrintSetup.FitHeight = 0;

                if (!esRemito)
                {
                    // Configurar la cantidad de copias
                    hoja.PrintSetup.Copies = 2;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}

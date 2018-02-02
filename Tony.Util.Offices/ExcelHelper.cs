using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Tony.Util.Offices
{
    /// <summary>
    /// NPOI Excel DataTable操作类
    /// </summary>
   public class ExcelHelper
    {
        #region DataTable导出到Excel的MemoryStream
        /// <summary>
        /// DataTable导出到Excel的MemoryStream Export()
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        public static MemoryStream ExportMemoryStream(DataTable dtSource, ExcelConfig excelConfig)
        {
            var colInt = 0;
            for (int i = 0; i < dtSource.Columns.Count;)
            {
                var column = dtSource.Columns[i];
                if (excelConfig.ColumnEntity[i].Column != column.ColumnName)
                {
                    dtSource.Columns.Remove(column.ColumnName);
                }
                else
                {
                    i++;
                }
            }
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();

            #region 右击文件属性信息
            {
                var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;
                var si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "Tony";
                si.ApplicationName = "Tony";
                si.LastAuthor = "Tony";
                si.Comments = "Tony";
                si.Title = "标题信息";
                si.Subject = "主题信息";
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 设置标题样式
            var headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            if (excelConfig.Background != new Color())
            {
                headStyle.FillPattern = FillPattern.SolidForeground;
                headStyle.FillForegroundColor = GetColor(workbook, excelConfig.Background);
            }
            var font = workbook.CreateFont();
            font.FontHeightInPoints = excelConfig.TitlePoint;
            if (excelConfig.ForeColor != new Color()) font.Color = GetColor(workbook, excelConfig.ForeColor);
            font.Boldweight = 700;
            headStyle.SetFont(font);
            #endregion

            #region 设置列头及样式
            var colHeadStyle = workbook.CreateCellStyle();
            colHeadStyle.Alignment = HorizontalAlignment.Center;
            var colHeadFont = workbook.CreateFont();
            colHeadFont.FontHeightInPoints = excelConfig.HeadPoint;
            colHeadStyle.SetFont(font);
            #endregion

            #region 设置内容单元格样式
            var arrColStyle = new ICellStyle[dtSource.Columns.Count];
            var arrColWidth = new int[dtSource.Columns.Count];
            var arrColName = new string[dtSource.Columns.Count];
            foreach (DataColumn col in dtSource.Columns)
            {
                var colStyle = workbook.CreateCellStyle();
                colStyle.Alignment = HorizontalAlignment.Center;
                arrColWidth[col.Ordinal] = Encoding.GetEncoding(936).GetBytes(col.ColumnName).Length;
                arrColName[col.Ordinal] = col.ColumnName;
                if (excelConfig.ColumnEntity != null)
                {
                    var colEntity = excelConfig.ColumnEntity.Find(t => t.Column == col.ColumnName);
                    if (colEntity != null)
                    {
                        arrColName[col.Ordinal] = colEntity.ExcelColumn;
                        if (colEntity.Width != 0) arrColWidth[col.Ordinal] = colEntity.Width;
                        if (colEntity.Background != new Color())
                        {
                            colStyle.FillPattern = FillPattern.SolidForeground;
                            colStyle.FillForegroundColor = GetColor(workbook, colEntity.Background);
                        }
                        if (colEntity.Font != null || colEntity.Point != 0 || colEntity.ForeColor != new Color())
                        {
                            var colFont = workbook.CreateFont();
                            colFont.FontHeightInPoints = 10;
                            if (colEntity.Font != null) colFont.FontName = colEntity.Font;
                            if (colEntity.Point != 0) colFont.FontHeightInPoints = colEntity.Point;
                            if (colEntity.ForeColor != new Color())
                                colFont.Color = GetColor(workbook, colEntity.ForeColor);
                            colStyle.SetFont(font);
                        }
                        colStyle.Alignment = GetAlignment(colEntity.Alignment);
                    }
                }
                arrColStyle[col.Ordinal] = colStyle;
            }
            if (excelConfig.IsAllSizeColumn)
            {
                #region 根据列中最长列的长度取得列宽
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        if (arrColWidth[j] != 0)
                        {
                            var intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                            if (intTemp > arrColWidth[j]) arrColWidth[j] = intTemp;
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region 填充数据       
            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");
            var rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0) sheet = workbook.CreateSheet();

                    #region 表头及样式
                    {
                        if (excelConfig.Title != null)
                        {
                            var headerRow = sheet.CreateRow(0);
                            if (excelConfig.TitleHeight != 0)
                            {
                                headerRow.Height = (short)(excelConfig.TitleHeight * 20);
                            }
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(excelConfig.Title);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(
                                new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                        }
                    }
                    #endregion

                    #region 列头及样式
                    {
                        var headerRow = sheet.CreateRow(1);
                        #region 如果设置了列标题就按列标题定义列头，没定义直接按字段名输出
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(arrColName[column.Ordinal]);
                            headerRow.GetCell(column.Ordinal).CellStyle = colHeadStyle;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                #region 填充内容
                var dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    var newCell = dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle = arrColStyle[column.Ordinal];
                    var drValue = row[column].ToString();
                    SetCell(newCell, dateStyle, column.DataType, drValue);
                }
                #endregion

                rowIndex++;
            }
            #endregion

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        #endregion

        #region List根据模板导出ExcelMemoryStream
        /// <summary>
        /// List根据模板导出ExcelMemoryStream
        /// </summary>
        /// <param name="list"></param>
        /// <param name="templdateName"></param>
        public static MemoryStream ExportListByTempale(List<TemplateMode> list, string templdateName)
        {
            var templatePath = HttpContext.Current.Server.MapPath("/") + "/Resource/ExcelTemplate";
            var templateName1 = string.Format("{0}{1}", templatePath, templdateName);
            var fs = new FileStream(templateName1, FileMode.Open, FileAccess.Read);
            ISheet sheet;
            if (templdateName.IndexOf(".xlsx", StringComparison.Ordinal) == -1)
            {
                var hssfworkbook = new HSSFWorkbook(fs);
                sheet = hssfworkbook.GetSheetAt(0);
                SetPurchaseOrder(sheet, list);
                sheet.ForceFormulaRecalculation = true;
                using (var ms = new MemoryStream())
                {
                    hssfworkbook.Write(ms);
                    ms.Flush();
                    return ms;
                }
            }
            var xssfworkbook = new XSSFWorkbook(fs);
            sheet = xssfworkbook.GetSheetAt(0);
            SetPurchaseOrder(sheet, list);
            sheet.ForceFormulaRecalculation = true;
            using (var ms = new MemoryStream())
            {
                xssfworkbook.Write(ms);
                ms.Flush();
                return ms;
            }
        }

        /// <summary>
        /// 赋值单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="list"></param>
        private static void SetPurchaseOrder(ISheet sheet, List<TemplateMode> list)
        {
            try
            {
                foreach (var item in list)
                {
                    var row = sheet.GetRow(item.row) ?? sheet.CreateRow(item.row);
                    var cell = row.GetCell(item.cell) ?? row.CreateCell(item.cell);
                    cell.SetCellValue(item.value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion

        #region Excel导出下载
        /// <summary>
        /// Excel导出下载
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        public static void ExcelDownload(DataTable dtSource, ExcelConfig excelConfig)
        {
            var ms = ExportMemoryStream(dtSource, excelConfig);
            var curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(excelConfig.FileName, Encoding.UTF8));
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// Excel导出下载
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="templdateName">模板文件名</param>
        /// <param name="newFileName">文件名</param>
        public static void ExcelDownload(List<TemplateMode> list, string templdateName, string newFileName)
        {
            var ms = ExportListByTempale(list, templdateName);
            var response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "UTF-8";
            response.ContentType = "application/vnd-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=" + newFileName);
            response.BinaryWrite(ms.ToArray());
            response.End();
        }
        #endregion

        #region Excel导入
        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ExcelImport(string strFileName)
        {
            var dt = new DataTable();
            ISheet sheet;
            using (var fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (strFileName.IndexOf(".xlsx", StringComparison.Ordinal) == -1)
                {
                    var hssfworkbook = new HSSFWorkbook(fs);
                    sheet = hssfworkbook.GetSheetAt(0);
                }
                else
                {
                    var xssfworkbook = new XSSFWorkbook();
                    sheet = xssfworkbook.GetSheetAt(0);
                }
            }
            var rows = sheet.GetRowEnumerator();
            var headRow = sheet.GetRow(0);
            var cellCount = headRow.LastCellNum;
            for (int i = 0; i < cellCount; i++)
            {
                var cell = headRow.GetCell(i);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var dataRow = dt.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null) dataRow[j] = row.GetCell(j).ToString();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        } 
        #endregion

        #region DataTable导出到Excel文件excelConfig中FileName设置为全路径
        /// <summary>
        /// DataTable导出到Excel文件excelConfig中FileName设置为全路径
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        public static void ExcelExport(DataTable dtSource, ExcelConfig excelConfig)
        {
            using (var ms = ExportMemoryStream(dtSource,excelConfig))
            {
                using (var fs = new FileStream(excelConfig.FileName,FileMode.Create,FileAccess.Write))
                {
                    var data = ms.ToArray();
                    fs.Write(data,0,data.Length);
                    fs.Flush();
                }
            }
        } 
        #endregion

        #region 设置表格内容
        private static void SetCell(ICell newCell, ICellStyle dateStyle, Type dataType, string drValue)
        {
            switch (dataType.ToString())
            {
                case "System.String"://字符串类型
                    newCell.SetCellValue(drValue);
                    break;
                case "System.DateTime"://日期类型
                    DateTime dateV;
                    if (DateTime.TryParse(drValue, out dateV))
                    {
                        newCell.SetCellValue(dateV);
                    }
                    else
                    {
                        newCell.SetCellValue("");
                    }
                    newCell.CellStyle = dateStyle;
                    break;
                case "System.Boolean"://布尔类型
                    bool boolV;
                    bool.TryParse(drValue, out boolV);
                    newCell.SetCellValue(boolV);
                    break;
                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV;
                    int.TryParse(drValue, out intV);
                    newCell.SetCellValue(intV);
                    break;
                case "System.Decimal"://浮点型
                case "System.Double":
                    double doubleV;
                    double.TryParse(drValue, out doubleV);
                    newCell.SetCellValue(doubleV);
                    break;
                case "System.DBNull"://空值处理
                    newCell.SetCellValue("");
                    break;
                default:
                    newCell.SetCellValue("");
                    break;
            }
        }
        #endregion

        #region RGB颜色转NPOI颜色

        private static short GetColor(HSSFWorkbook workbook, Color systemColor)
        {
            short s = 0;
            var xlPalette = workbook.GetCustomPalette();
            var xlColor = xlPalette.FindColor(systemColor.R, systemColor.G, systemColor.B);
            if (xlColor == null)
            {
                xlColor = xlPalette.FindSimilarColor(systemColor.R, systemColor.G, systemColor.B);
                s = xlColor.Indexed;
            }
            else
            {
                s = xlColor.Indexed;
            }
            return s;
        }
        #endregion

        #region 设置列的对齐方式

        private static HorizontalAlignment GetAlignment(string style)
        {
            switch (style)
            {
                case "center":
                    return HorizontalAlignment.Center;
                case "left":
                    return HorizontalAlignment.Left;
                case "right":
                    return HorizontalAlignment.Right;
                case "fill":
                    return HorizontalAlignment.Fill;
                case "justify":
                    return HorizontalAlignment.Justify;
                case "centerselection":
                    return HorizontalAlignment.CenterSelection;
                case "distributed":
                    return HorizontalAlignment.Distributed;
            }
            return HorizontalAlignment.General;
        }
        #endregion
    }
}

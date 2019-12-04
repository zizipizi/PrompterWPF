using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PrompterWPF.Interfaces;

namespace PrompterWPF.Model
{
    public class SlidesRepository : ISlideRepository
    {
        private string filePath = Environment.CurrentDirectory + "/" + "Test.xlsx";
        private XSSFWorkbook workbook;
        private ISheet sheet;

        public List<Slide> slides = new List<Slide>();

        public SlidesRepository()
        {
            OpenExcelFile(filePath);
        }

        public void OpenExcelFile(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
                sheet = workbook.GetSheetAt(0);
            }
        }

        private int GetNumberOfSheets()
        {
            return workbook.NumberOfSheets;
        }

        private string GetDataFromExcel(int row, int cell)
        {
            return sheet.GetRow(row).GetCell(cell)?.ToString();
        }

        private string [] GetSlideTexts(int row, int cell, string currentValue)
        {
            List<string> texts = new List<string>();
            
            for (int i = row; i < sheet.PhysicalNumberOfRows; i++)
            {
                if (GetDataFromExcel(i, cell) != null && currentValue == GetDataFromExcel(row, cell))
                {
                    texts.Add(GetDataFromExcel(i, cell + 2));
                }
            }
            return texts.ToArray();
        }

        private bool IsCellExists(int row, int cell)
        {
            if (sheet.GetRow(row).GetCell(cell) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public (IReadOnlyCollection<Slide> slides, int timer) GetSlides()
        {
            var cell = 0;
            var slides = new List<Slide>();
            int slideCounter = 1;
            int timer = GetPresentationTimer();

            for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)
            {
                if (IsCellExists(i, cell) && (GetDataFromExcel(i, cell) != GetDataFromExcel(i - 1, cell)))
                {
                    slides.Add(new Slide
                    {
                        Label = GetDataFromExcel(i, cell + 1),
                        Texts = GetSlideTexts(i, cell, GetDataFromExcel(i, cell)),
                        ImagePath = GetDataFromExcel(i, cell + 4),
                        Number = slideCounter++
                    });
                }
            }
            return (slides, timer);
        }

        public void SetSheet(int index)
        {
            sheet = workbook.GetSheetAt(index);
        }

        public bool IsSheetExists(int index)
        {
            if (index < GetNumberOfSheets())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetPresentationTimer()
        {
            int timer = 0;
            int.TryParse(GetDataFromExcel(1, 3), out timer);
            return timer;
        }
    }

    public class Slide
    {
        public string Label { get; set; }

        public IReadOnlyCollection<string> Texts { get; set; }

        public int Number { get; set; }

        public string ImagePath { get; set; }
    }


    public class SheetInfo : INotifyPropertyChanged
    {
        public int timerTime;


        public int TimerTime
        {
            get { return timerTime; }
            set
            {
                timerTime = value;
                OnPropertyChanged("TimerTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}

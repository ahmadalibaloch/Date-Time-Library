using System;
using System.Collections.Generic;
using System.Text;

namespace HijriLib
{
    public class HijriDateTime
    {
        private int mainDate;
        private int mainMonth;
        private int mainYear;
        private string mainDateStr = "";
        private int DateDiff = 0;
        private bool changMonthG = false;
        private bool changYearG = false;
        private bool changMonthH = false;
        private bool changYearH = false;
        HijriErrorEventArgs HEA = new HijriErrorEventArgs("An Error Please read help.");
        public delegate void setErr(object sender, HijriErrorEventArgs HEA);
        public delegate void showEvent(object sender, HijriErrorEventArgs HEA);
        public event setErr SetError;
        public event showEvent ShowEvent;
        System.Collections.Specialized.NameValueCollection NVC = new System.Collections.Specialized.NameValueCollection();
        HijriConversion HConversion = new HijriConversion();
        /// <summary>
        /// PROPERTIES OF MY HIJRI DATE TIME CLASS
        /// </summary>
        public HijriDateTime()
        {
            mainDateStr = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            mainDate = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Day;
            mainMonth = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Month;
            mainYear = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Year;
            mainDateStr = mainMonth + "/" + mainDate + "/" + mainYear;
        }

        public int SetDate
        {
            set
            {
                if (value > LastDateG(mainMonth, mainYear) | value < 1)
                {
                    HEA.HijriError = "Enter Currect Date for this Month between " + LastDateG(mainMonth, mainYear) + " and 1.\nNow setting defult date.";
                    SetError(this, HEA);
                }
                else
                {
                    mainDate = value;
                }
            }
            get { return mainDate; }
        }
        public int SetMonth
        {
            set
            {
                if (value > 12 | value < 1)
                {
                    HEA.HijriError = "Enter Currect month between 12 and 1.";
                    SetError(this, HEA);
                }
                else
                {
                    mainMonth = value;
                }
            }
            get { return mainMonth; }
        }
        public int SetYear
        {
            set { if (value > 1)mainYear = value; else { HEA.HijriError = "Enter valid year.\nSetting defult year."; SetError(this, HEA); } }
            get { return mainYear; }
        }

        public int DateDifference
        {
            get
            {
                return DateDiff;
            }
            set
            {
                if (value > -20 & value < 20)
                { DateDiff = value; }
                else
                {
                    HEA.HijriError = "Set valid difference between -20 and 20.";
                    SetError(this, HEA);
                }
            }
        }

        public int GetDay
        {
            get
            {
                return Day();
            }
        }
        public int GetMonth
        {
            get
            {
                return Month();
            }
        }
        public int GetYear
        {
            get
            {
                return Year();
            }
        }
        public string GetLongDate
        {
            get
            {
                return LongDate();
            }
        }
        public string GetShortDate
        {
            get
            {
                return ShortDate();
            }
        }
        public string GetCompleteDate
        {
            get
            {
                return CompleteDate();
            }
        }
        public string GetShortDateTime
        {
            get
            {
                return ShortDateTime();
            }
        }
        public string GetLongDateTime
        {
            get
            {
                return LongDateTime();
            }
        }
        public string GetCompleteDateTime
        {
            get
            {
                return CompleteDateTime();
            }
        }
        /// <summary>
        ///  METHODS OF MY HIJRI DATE TIME CLASS
        /// </summary>
        internal int GetDate_G_Back(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date - DateDiffInteger >= 1)
            {
                RDate = date - DateDiffInteger;
            }
            else
            {
                changMonthG = true;
                if (Month > 1)
                    RDate = LastDateG(Month - 1, Year) + (date - DateDiffInteger);
                else
                    RDate = LastDateH(12, Year - 1);
            }
            return RDate;
        }
        internal int GetDate_G_Next(int date, int DateDiffInteger, int month, int year)
        {
            int rDate = 0;
            if (date + DateDiffInteger <= LastDateG(month, year))
            {
                rDate = date + DateDiffInteger;
            }
            else
            {
                changMonthG = true;
                rDate = DateDiffInteger - (LastDateG(month, year) - date);
            }
            return rDate;
        }
        internal int GetMonth_G_Back(int CurrentMonth)
        {
            if (changMonthG)
            {
                changMonthG = false;
                if (CurrentMonth == 1)
                {
                    changYearG = true;
                    return 12;

                }
                else
                {
                    return CurrentMonth - 1;
                }
            }
            else
            {
                return CurrentMonth;
            }
        }
        internal int GetMonth_G_Next(int CurrentMonth)
        {
            if (changMonthG)
            {
                changMonthG = false;
                if (CurrentMonth == 12)
                {
                    changYearG = true;
                    return 1;

                }
                else
                {
                    return CurrentMonth + 1;
                }
            }
            else
            {
                return CurrentMonth;
            }
        }
        internal int GetYear_G_Back(int CurrentYear)
        {
            if (changYearG)
            {
                changYearG = false;
                return CurrentYear - 1;
            }
            else
            { return CurrentYear; }
        }
        internal int GetYear_G_Next(int CurrentYear)
        {
            if (changYearG)
            {
                changYearG = false;
                return CurrentYear + 1;
            }
            else
            { return CurrentYear; }
        }
        internal int GetDate_H_Back(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date - DateDiffInteger >= 1)
            {
                RDate = date - DateDiffInteger;
            }
            else
            {
                changMonthH = true;
                if (Month > 1)
                    RDate = LastDateH(Month - 1, Year);
                else
                    RDate = LastDateH(12, Year - 1);
            }
            return RDate;
        }
        internal int GetDate_H_Next(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date + DateDiffInteger <= LastDateH(Month, Year))
            {
                RDate = date + DateDiffInteger;
            }
            else
            {
                changMonthH = true;
                RDate = DateDiffInteger - (LastDateH(Month, Year) - date);

            }
            return RDate;
        }
        internal int GetMonth_H_Back(int CurrentMonth)
        {
            if (changMonthH)
            {
                changMonthH = false;
                if (CurrentMonth == 1 | CurrentMonth == 01)
                {
                    changYearH = true;
                    return 12;
                }
                else
                {
                    return CurrentMonth - 1;
                }
            }
            else
            {
                return CurrentMonth;
            }

        }
        internal int GetMonth_H_Next(int CurrentMonth)
        {
            if (changMonthH)
            {
                changMonthH = false;
                if (CurrentMonth == 12)
                {
                    changYearH = true;
                    return 1;
                }
                else
                {
                    return CurrentMonth + 1;
                }
            }
            else
            {
                return CurrentMonth;
            }

        }
        internal int GetYear_H_Back(int CurrentYear)
        {
            if (changYearH)
            {
                changYearH = false;
                return CurrentYear - 1;
            }
            else
            { return CurrentYear; }

        }
        internal int GetYear_H_Next(int CurrentYear)
        {
            if (changYearH)
            {
                changYearH = false;
                return CurrentYear + 1;
            }
            else
            { return CurrentYear; }

        }

        internal string GetChangedDateH(int day, int dateDiffInteger, int month, int year)
        {
            if (dateDiffInteger >= 1)
            {
                day = GetDate_H_Next(day, dateDiffInteger, month, year);
                month = GetMonth_H_Next(month);
                year = GetYear_H_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_H_Back(day, dateDiffInteger, month, year);
                month = GetMonth_H_Back(month);
                year = GetYear_H_Back(year);
            }
            return month + "/" + day + "/" + year;
        }
        internal string GetChangedDateH(string dateString_mm_dd_yy, int dateDiffInteger)
        {
            int outDay = DateTime.Parse(dateString_mm_dd_yy).Day;
            int outMonth = DateTime.Parse(dateString_mm_dd_yy).Month;
            int outYear = DateTime.Parse(dateString_mm_dd_yy).Year;
            if (dateDiffInteger >= 1)
            {
                outDay = GetDate_H_Next(outDay, dateDiffInteger, outMonth, outYear);
                outMonth = GetMonth_H_Next(outMonth);
                outYear = GetYear_H_Next(outYear);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                outDay = GetDate_H_Back(outDay, dateDiffInteger, outMonth, outYear);
                outMonth = GetMonth_H_Back(outMonth);
                outYear = GetYear_H_Back(outYear);
            }
            return outMonth + "/" + outDay + "/" + outYear;
        }
        internal string GetChangedDateG(int day, int dateDiffInteger, int month, int year)
        {
            if (dateDiffInteger >= 1)
            {
                day = GetDate_G_Next(day, dateDiffInteger, month, year);
                month = GetMonth_G_Next(month);
                year = GetYear_G_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_G_Back(day, dateDiffInteger, month, year);
                month = GetMonth_G_Back(month);
                year = GetYear_G_Back(year);
            }
            return month + "/" + day + "/" + year;
        }
        internal string GetChangedDateG(string dateString_mm_dd_yy, int dateDiffInteger)
        {
            int day = DateTime.Parse(dateString_mm_dd_yy).Day;
            int month = DateTime.Parse(dateString_mm_dd_yy).Month;
            int year = DateTime.Parse(dateString_mm_dd_yy).Year;
            if (dateDiffInteger >= 1)
            {
                day = GetDate_G_Next(day, dateDiffInteger, month, year);
                month = GetMonth_G_Next(month);
                year = GetYear_G_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_G_Back(day, dateDiffInteger, month, year);
                month = GetMonth_G_Back(month);
                year = GetYear_G_Back(year);
            }
            return month + "/" + day + "/" + year;
        }

        internal int LastDateG(int GregMonth, int GregYear)
        {
            int rDate = 0;
            switch (GregMonth)
            {
                case 1:
                    rDate = 31;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(GregYear))
                        rDate = 29;
                    else
                        rDate = 28;
                    break;
                case 3:
                    rDate = 31;
                    break;
                case 4:
                    rDate = 30;
                    break;
                case 5:
                    rDate = 31;
                    break;
                case 6:
                    rDate = 30;
                    break;
                case 7:
                    rDate = 31;
                    break;
                case 8:
                    rDate = 31;
                    break;
                case 9:
                    rDate = 30;
                    break;
                case 10:
                    rDate = 31;
                    break;
                case 11:
                    rDate = 30;
                    break;
                case 12:
                    rDate = 31;
                    break;
            }
            return rDate;
        }
        internal int LastDateH(int HijriMonth, int HijriYear)
        {
            System.Globalization.HijriCalendar HijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar GregCalendar = new System.Globalization.GregorianCalendar();
            int day = 0, newDay = 0;
            int month, newMonth;
            int year, newYear;
            if (HijriMonth == 12)
            {
                DateTime HijriDateTime = new DateTime(HijriYear + 1, 1, 1, HijriCalendar);
                day = GregCalendar.GetDayOfMonth(HijriDateTime);
                month = GregCalendar.GetMonth(HijriDateTime);
                year = GregCalendar.GetYear(HijriDateTime);
            }
            else
            {
                DateTime HijriDateTime = new DateTime(HijriYear, HijriMonth + 1, 1, HijriCalendar);
                day = GregCalendar.GetDayOfMonth(HijriDateTime);
                month = GregCalendar.GetMonth(HijriDateTime);
                year = GregCalendar.GetYear(HijriDateTime);
            }
            newDay = GetDate_G_Back(day, 1, month, year);
            newMonth = GetMonth_G_Back(month);
            newYear = GetYear_G_Back(year);
            string Gdate = HConversion.GregToHijri(newDay, newMonth, newYear, false);
            string ourStr = "";
            bool start = false;
            foreach (char ch in Gdate)
            {
                if (ch == '/')
                {
                    start = !start;
                }
                else
                    if (start)
                        ourStr += ch;

            }
            return Int32.Parse(ourStr);
        }

        internal int CheckDateH(int date, int month, int year)
        {
            if (month > 12 | month < 1) { month = GetMonth; }
            if (date > LastDateH(month, year) | date < 1)
            {
                HEA.HijriError = "Enter valid date for this month between " + LastDateH(month, year) + " and 1.\nSetting Defult date.";
                SetError(this, HEA);
                return GetDay;
            }
            else
            {
                return date;
            }
        }
        internal int CheckMonthH(int month)
        {
            if (month > 12 | month < 1)
            {
                HEA.HijriError = "Enter valid month between 12 and 1.\nSetting Defult month.";
                SetError(this, HEA);
                return GetMonth;
            }
            else
            {
                return month;
            }
        }
        internal int CheckYearH(int year)
        {
            if (year < 1 | year > 9666)
            {
                HEA.HijriError = "Enter valid year between 1 and 9666.\nSetting Defult year.";
                SetError(this, HEA);
                return GetYear;
            }
            else
            {
                return year;
            }
        }
        internal int CheckDateG(int date, int month, int year)
        {
            if (month > 12 | month < 1) { month = DateTime.Now.Month; }
            if (date > LastDateG(month, year) | date < 1)
            {
                HEA.HijriError = "Enter valid date for this month between " + LastDateG(month, year) + " and 1.\nSetting Defult date.";
                SetError(this, HEA);
                return DateTime.Now.Day;
            }
            else
            {
                return date;
            }
        }
        internal int CheckMonthG(int month)
        {
            if (month > 12 | month < 1)
            {
                HEA.HijriError = "Enter valid month between 12 and 1.\nSetting Defult month.";
                SetError(this, HEA);
                return DateTime.Now.Month;
            }
            else
            {
                return month;
            }
        }
        internal int CheckYearG(int year)
        {
            if (year < 1 | year > 9666)
            {
                HEA.HijriError = "Enter valid year between 1 and 9666.\nSetting Defult year.";
                SetError(this, HEA);
                return DateTime.Now.Year;
            }
            else
            {
                return year;
            }
        }

        internal int Day()
        {
            return DateTime.Parse(HConversion.GregToHijri(mainDate, mainMonth, mainYear, false)).Day;
        }
        internal int Month()
        {
            return DateTime.Parse(HConversion.GregToHijri(mainDate, mainMonth, mainYear, false)).Month;
        }
        internal int Year()
        {
            return DateTime.Parse(HConversion.GregToHijri(mainDate, mainMonth, mainYear, false)).Year;
        }

        internal string MonthName()
        {
            return System.Enum.GetName(typeof(HijriMonthNames), Month());
        }
        internal string DayName()
        {
            System.Globalization.HijriCalendar myHijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar myGregorianCalendar = new System.Globalization.GregorianCalendar();
            DateTime newg = new DateTime(mainYear, mainMonth, mainDate, myGregorianCalendar);
            DayOfWeek HDayofWeek = myHijriCalendar.GetDayOfWeek(newg);
            return HDayofWeek.ToString();
        }
        internal string LongDate()
        {
            return Month() + "/" + Day() + "/" + Year();
        }
        internal string ShortDate()
        {
            return Month() + "/" + Day() + "/" + Year().ToString().Remove(0, 2);
        }
        internal string CompleteDate()
        {
            return DayName() + ", " + MonthName() + " " + Day() + ", " + Year();
        }
        internal string ShortDateTime()
        {
            return Month() + "/" + Day() + "/" + Year().ToString().Remove(0, 2) + " " + DateTime.Now.ToShortTimeString();

        }
        internal string LongDateTime()
        {
            return Month() + "/" + Day() + "/" + Year() + " " + DateTime.Now.ToShortTimeString();

        }
        internal string CompleteDateTime()
        {
            return DayName() + ", " + MonthName() + " " + Day() + ", " + Year() + " " + DateTime.Now.ToShortTimeString();
        }
        /// <summary>
        /// CONVERTION AND ETC METHOD OF MY HIJRI DATE TIME CLASS
        /// </summary>
        public void Refresh()
        {
            mainDateStr = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            mainDate = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Day;
            mainMonth = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Month;
            mainYear = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Year;
            mainDateStr = mainMonth + "/" + mainDate + "/" + mainYear;

        }
        public override string ToString()
        {
            return (Day() + ", " + MonthName() + ", " + Year());
        }
        public string MonthDate(int HijriMonth, int HijriYear, bool MonthName)
        {
            if (MonthName)
            { return HConversion.HijriToGreg(01, HijriMonth, HijriYear, true); }
            else
            {
                return HConversion.HijriToGreg(01, HijriMonth, HijriYear, false);
            }
        }
        public void AddEvent(string Name, string Greg_mm_dd_yyyy)
        {
            try
            {
                int day = DateTime.Parse(Greg_mm_dd_yyyy).Day;
                int month = DateTime.Parse(Greg_mm_dd_yyyy).Month;
                int year = DateTime.Parse(Greg_mm_dd_yyyy).Year;
                DateTime cDateTime = new DateTime(year, month, day);
                DateTime nDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (cDateTime <= nDateTime)
                {
                    HEA.HijriError = "The date you entered is passed.";
                    SetError(this, HEA);
                }
                else
                {
                    NVC.Set(Name, Greg_mm_dd_yyyy);
                }
            }
            catch { HEA.HijriError = "Enter valid date string formated mm_dd_yyyy."; SetError(this, HEA); }

        }
        public void AddEvent(string Name, int GregDay, int GregMonth, int GregYear)
        {
            try
            {
                DateTime cDateTime = new DateTime(GregYear, GregMonth, GregDay);
                DateTime nDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (cDateTime <= nDateTime)
                {
                    HEA.HijriError = "The date you entered is passed.";
                    SetError(this, HEA);
                }
                else
                {
                    NVC.Set(Name, cDateTime.ToShortDateString());
                }
            }
            catch { HEA.HijriError = "Enter each date part currect."; SetError(this, HEA); }

        }
        public void AddEventHijri(string Name, string HijriDate_mm_dd_yyyy)
        {

            try
            {
                string tStrFG = HConversion.HijriToGreg(HijriDate_mm_dd_yyyy, false);
                int day = DateTime.Parse(tStrFG).Day;
                int month = DateTime.Parse(tStrFG).Month;
                int year = DateTime.Parse(tStrFG).Year;
                DateTime cDateTime = new DateTime(year, month, day);
                DateTime nDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (cDateTime <= nDateTime)
                {
                    HEA.HijriError = "The date you entered is passed.";
                    SetError(this, HEA);
                }
                else
                {
                    NVC.Set(Name, cDateTime.ToShortDateString());
                }
            }
            catch { HEA.HijriError = "Enter valid date string formated mm_dd_yyyy."; SetError(this, HEA); }

        }
        public string AddEventHijri(string Name, int HijriDay, int HijriMonth, int HijriYear)
        {
            string rStr = "";
            string tStrFG = HConversion.HijriToGreg(HijriDay, HijriMonth, HijriYear, false);
            try
            {
                int day = DateTime.Parse(tStrFG).Day;
                int month = DateTime.Parse(tStrFG).Month;
                int year = DateTime.Parse(tStrFG).Year;

                DateTime cDateTime = new DateTime(year, month, day);
                DateTime nDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (cDateTime <= nDateTime)
                {
                    HEA.HijriError = "The date you entered is passed.";
                    SetError(this, HEA);
                }
                else
                {
                    rStr = cDateTime.ToShortDateString();
                    NVC.Set(Name, cDateTime.ToShortDateString());
                }
            }
            catch { HEA.HijriError = "Enter valid date string formated mm_dd_yyyy."; SetError(this, HEA); }
            return rStr;
        }
        public void CheckEvent()
        {
            DateTime nowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            for (int i = 0; i < NVC.Count; i++)
            {
                int day = DateTime.Parse(NVC.Get(i)).Day;
                int month = DateTime.Parse(NVC.Get(i)).Month;
                int year = DateTime.Parse(NVC.Get(i)).Year;
                DateTime TDateTime = new DateTime(year, month, day);
                if (TDateTime == nowDateTime)
                {
                    HEA.EventMessage = NVC.GetKey(i);
                    NVC.Remove(NVC.GetKey(i));
                    ShowEvent(this, HEA);

                }
            }
        }
        public string[,] GetEvents()
        {
            string[,] StrEvent = new string[2, NVC.Count];
            int Loop = NVC.Count;
            int i = 0;
            while (Loop > 0)
            {
                StrEvent[0, i] = NVC[i];
                Loop--;
                i++;
            }
            Loop = NVC.Count;
            i = 0;
            while (Loop > 0)
            {
                StrEvent[1, i] = NVC.GetKey(i);
                Loop--;
                i++;
            }
            return StrEvent;
        }
        public void SetEvents(string[,] StrEvent)
        {
            NVC.Clear();
            try
            {
                int Loop = (StrEvent.Length) / 2;
                int i = 0;
                while (Loop > 0)
                {
                    NVC.Set(StrEvent[1, i], StrEvent[0, i]);
                    i++;
                    Loop--;
                }
            }
            catch
            {
                HEA.HijriError = "Con't set events becouse of an error in given data or 2D array's elements or not in currect format.";
                SetError(this, HEA);
            }

        }
    }
    public class HijriConversion
    {
        private int mainDate;
        private int mainMonth;
        private int mainYear;
        private string mainDateStr = "";
        private int DateDiff = 0;
        private bool changMonthG = false;
        private bool changYearG = false;
        private bool changMonthH = false;
        private bool changYearH = false;
        HijriErrorEventArgs HEA = new HijriErrorEventArgs("An Error Please read help.");
        public delegate void setErr(object sender, HijriErrorEventArgs HEA);
        System.Collections.Specialized.NameValueCollection NVC = new System.Collections.Specialized.NameValueCollection();
        public event setErr SetError;
        /// <summary>
        /// Loads current date to mainDate, mainMonth and mainYear with addition of DateDiff
        /// </summary>
        public HijriConversion()
        {
            mainDateStr = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            mainDate = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Day;
            mainMonth = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Month;
            mainYear = DateTime.Parse(GetChangedDateG(mainDateStr, DateDiff)).Year;
            mainDateStr = mainMonth + "/" + mainDate + "/" + mainYear;
        }

        public int DateDifference
        {
            get
            {
                return DateDiff;
            }
            set
            {
                if (value > -20 & value < 20)
                { DateDiff = value; }
                else
                {
                    HEA.HijriError = "Set valid difference between -20 and 20.";
                    SetError(this, HEA);
                }
            }
        }


        /// <summary>
        ///  Gets day from the date string. i.e middle of the string part. e.g it gives 29 from "01-29-2010"
        /// </summary>
        internal int dateParse(string dateStr)
        {
            string outStr = "";
            bool start = false;
            foreach (char ch in dateStr)
            {
                if (ch == '/')
                {
                    start = !start;
                }
                else
                    if (start)
                        outStr += ch;

            }
            return Int32.Parse(outStr);
        }
        /// <summary>
        ///  Gets month from the date string. i.e first of the string part. e.g it gives 01 from "01-29-2010"
        /// </summary>
        internal int monthParse(string dateStr)
        {
            string outStr = "";
            foreach (char ch in dateStr)
            {
                if (ch == '/')
                {
                    return Int32.Parse(outStr);
                }
                else
                    outStr += ch;
            }
            return Int32.Parse(outStr);
        }
        /// <summary>
        ///  Gets year from the date string. i.e end of the string part. e.g it gives 2010 from "01-29-2010"
        /// </summary>
        internal int yearParse(string dateStr)
        {
            char[] str = { '/' };
            int startIndex = dateStr.LastIndexOfAny(str);
            return Int32.Parse(dateStr.Substring(startIndex + 1));
        }

        /// <summary>
        /// Returns back Gregorian date integer of the 'int date' in the arguments. If changed integer is in Back month instead of current. It issues a change to month in the GetMonthGBack() function.
        /// </summary>
        internal int GetDate_G_Back(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date - DateDiffInteger >= 1)
            {
                RDate = date - DateDiffInteger;
            }
            else
            {
                changMonthG = true;
                if (Month > 1)
                    RDate = LastDateG(Month - 1, Year) + (date - DateDiffInteger);
                else
                    RDate = LastDateH(12, Year - 1);
            }
            return RDate;
        }
        internal int GetDate_G_Next(int date, int DateDiffInteger, int month, int year)
        {
            int rDate = 0;
            if (date + DateDiffInteger <= LastDateG(month, year))
            {
                rDate = date + DateDiffInteger;
            }
            else
            {
                changMonthG = true;
                rDate = DateDiffInteger - (LastDateG(month, year) - date);
            }
            return rDate;
        }
        /// <summary>
        /// Get Month Back if it recieves a change of 'month boolean' from GetDateGBack() function.
        /// </summary>
        internal int GetMonth_G_Back(int CurrentMonth)
        {
            if (changMonthG)
            {
                changMonthG = false;
                if (CurrentMonth == 1)
                {
                    changYearG = true;
                    return 12;

                }
                else
                {
                    return CurrentMonth - 1;
                }
            }
            else
            {
                return CurrentMonth;
            }
        }
        /// <summary>
        /// Get Month Next if it recieves a change of 'month boolean' from GetDateHNext() function.
        /// </summary>
        internal int GetMonth_G_Next(int CurrentMonth)
        {
            if (changMonthG)
            {
                changMonthG = false;
                if (CurrentMonth == 12)
                {
                    changYearG = true;
                    return 1;

                }
                else
                {
                    return CurrentMonth + 1;
                }
            }
            else
            {
                return CurrentMonth;
            }
        }
        /// <summary>
        /// Get Gregorain Year Back if it recieves a change of 'year boolean' from GetMonthGBack() function.
        /// </summary>
        internal int GetYear_G_Back(int CurrentYear)
        {
            if (changYearG)
            {
                changYearG = false;
                return CurrentYear - 1;
            }
            else
            { return CurrentYear; }
        }
        /// <summary>
        /// Get Gregorian Year Next if it recieves a change of 'year boolean' from GetMonthGNext() function.
        /// </summary>
        internal int GetYear_G_Next(int CurrentYear)
        {
            if (changYearG)
            {
                changYearG = false;
                return CurrentYear + 1;
            }
            else
            { return CurrentYear; }
        }
        /// <summary>
        /// Returns back Hijri date integer of the 'int date' in the arguments. If changed integer is in back month instead of current. It issues a change to month in the GetMonthHBack() function.
        /// </summary>
        internal int GetDate_H_Back(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date - DateDiffInteger >= 1)
            {
                RDate = date - DateDiffInteger;
            }
            else
            {
                changMonthH = true;
                if (Month > 1)
                    RDate = LastDateH(Month - 1, Year) + (date - DateDiffInteger);
                else
                    RDate = LastDateH(12, Year - 1);
            }
            return RDate;
        }
        /// <summary>
        /// Returns next Hijri date integer of the 'int date' in the arguments. If changed integer is in next month instead of current. It issues a change to month in the GetMonthHNext() function.
        /// </summary>
        internal int GetDate_H_Next(int date, int DateDiffInteger, int Month, int Year)
        {
            int RDate = date;
            if (date + DateDiffInteger <= LastDateH(Month, Year))
            {
                RDate = date + DateDiffInteger;
            }
            else
            {
                changMonthH = true;
                RDate = DateDiffInteger - (LastDateH(Month, Year) - date);

            }
            return RDate;
        }
        /// <summary>
        /// Get Hijri Month Back if it recieves a change of 'month boolean' from GetDateHBack() function.
        /// </summary>
        internal int GetMonth_H_Back(int CurrentMonth)
        {
            if (changMonthH)
            {
                changMonthH = false;
                if (CurrentMonth == 1 | CurrentMonth == 01)
                {
                    changYearH = true;
                    return 1;
                }
                else
                {
                    return CurrentMonth - 1;
                }
            }
            else
            {
                return CurrentMonth;
            }

        }
        /// <summary>
        /// Get Hijri Month Next if it recieves a change of 'month boolean' from GetDateHNext() function.
        /// </summary>
        internal int GetMonth_H_Next(int CurrentMonth)
        {
            if (changMonthH)
            {
                changMonthH = false;
                if (CurrentMonth == 12)
                {
                    changYearH = true;
                    return 1;
                }
                else
                {
                    return CurrentMonth + 1;
                }
            }
            else
            {
                return CurrentMonth;
            }

        }
        /// <summary>
        /// Get Hijri Year Back if it recieves a change of 'year boolean' from GetMonthHBack() function.
        /// </summary>
        internal int GetYear_H_Back(int CurrentYear)
        {
            if (changYearH)
            {
                changYearH = false;
                return CurrentYear - 1;
            }
            else
            { return CurrentYear; }

        }
        /// <summary>
        /// Get Year Next if it recieves a change of 'year boolean' from GetMonthHNext() function.
        /// </summary>
        internal int GetYear_H_Next(int CurrentYear)
        {
            if (changYearH)
            {
                changYearH = false;
                return CurrentYear + 1;
            }
            else
            { return CurrentYear; }

        }

        /// <summary>
        /// Returns Next date. e.g for 01-30-14310 output will be 02-01-1431
        /// </summary>
        public string NextHijriDate(string HijriDate)
        {
            int day = DateTime.Parse(HijriDate).Day; int month = DateTime.Parse(HijriDate).Month; int year = DateTime.Parse(HijriDate).Year;
            day = GetDate_H_Next(day, 1, month, year);
            month = GetMonth_H_Next(month);
            year = GetYear_H_Next(year);
            return month + "/" + day + "/" + year;
        }
        /// <summary>
        /// Returns Next date. e.g for 01-31-2010 output will be 02-01-2010
        /// </summary>
        public string NextGregDate(string GregDate)
        {
            int day = DateTime.Parse(GregDate).Day; int month = DateTime.Parse(GregDate).Month; int year = DateTime.Parse(GregDate).Year;
            day = GetDate_G_Next(day, 1, month, year);
            month = GetMonth_G_Next(month);
            year = GetYear_G_Next(year);
            return month + "/" + day + "/" + year;

        }

        /// <summary>
        ///  Gives Hijri date after adding to or subracting from the Hijri date comprising of day, month and year arguments, dateDiffInteger(according to sign)
        /// </summary>
        internal string GetChangedDateH(int day, int dateDiffInteger, int month, int year)
        {
            if (dateDiffInteger >= 1)
            {
                day = GetDate_H_Next(day, dateDiffInteger, month, year);
                month = GetMonth_H_Next(month);
                year = GetYear_H_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_H_Back(day, dateDiffInteger, month, year);
                month = GetMonth_H_Back(month);
                year = GetYear_H_Back(year);
            }
            return month + "/" + day + "/" + year;
        }
        /// <summary>
        ///  Gives Hijri date after adding to or subracting from the Hijri date of string dateString_mm_dd_yy, dateDiffInteger(according to sign)
        /// </summary>
        internal string GetChangedDateH(string dateString_mm_dd_yy, int dateDiffInteger)
        {
            int outDay = DateTime.Parse(dateString_mm_dd_yy).Day;
            int outMonth = DateTime.Parse(dateString_mm_dd_yy).Month;
            int outYear = DateTime.Parse(dateString_mm_dd_yy).Year;
            if (dateDiffInteger >= 1)
            {
                outDay = GetDate_H_Next(outDay, dateDiffInteger, outMonth, outYear);
                outMonth = GetMonth_H_Next(outMonth);
                outYear = GetYear_H_Next(outYear);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                outDay = GetDate_H_Back(outDay, dateDiffInteger, outMonth, outYear);
                outMonth = GetMonth_H_Back(outMonth);
                outYear = GetYear_H_Back(outYear);
            }
            return outMonth + "/" + outDay + "/" + outYear;
        }
        /// <summary>
        ///  Gives Gregorian date after adding to or subracting from the Gregorian date comprising of day, month and year arguments, dateDiffInteger(according to sign)
        /// </summary>
        internal string GetChangedDateG(int day, int dateDiffInteger, int month, int year)
        {
            dateDiffInteger = -dateDiffInteger;
            if (dateDiffInteger >= 1)
            {
                day = GetDate_G_Next(day, dateDiffInteger, month, year);
                month = GetMonth_G_Next(month);
                year = GetYear_G_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_G_Back(day, dateDiffInteger, month, year);
                month = GetMonth_G_Back(month);
                year = GetYear_G_Back(year);
            }
            return month + "/" + day + "/" + year;
        }
        /// <summary>
        ///  Gives Gregorian date after adding to or subracting from the Gregorian date of string dateString_mm_dd_yy, dateDiffInteger(according to sign)
        /// </summary>
        internal string GetChangedDateG(string dateString_mm_dd_yy, int dateDiffInteger)
        {
            dateDiffInteger = -dateDiffInteger;
            int day = DateTime.Parse(dateString_mm_dd_yy).Day;
            int month = DateTime.Parse(dateString_mm_dd_yy).Month;
            int year = DateTime.Parse(dateString_mm_dd_yy).Year;
            if (dateDiffInteger >= 1)
            {
                day = GetDate_G_Next(day, dateDiffInteger, month, year);
                month = GetMonth_G_Next(month);
                year = GetYear_G_Next(year);
            }
            else if (dateDiffInteger <= -1)
            {
                dateDiffInteger = dateDiffInteger + (dateDiffInteger * -2);
                day = GetDate_G_Back(day, dateDiffInteger, month, year);
                month = GetMonth_G_Back(month);
                year = GetYear_G_Back(year);
            }
            return month + "/" + day + "/" + year;
        }

        /// <summary>
        /// Returns last date of the given Gregorain Month in the given year
        /// </summary>
        internal int LastDateG(int GregMonth, int GregYear)
        {
            int rDate = 0;
            switch (GregMonth)
            {
                case 1:
                    rDate = 31;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(GregYear))
                        rDate = 29;
                    else
                        rDate = 28;
                    break;
                case 3:
                    rDate = 31;
                    break;
                case 4:
                    rDate = 30;
                    break;
                case 5:
                    rDate = 31;
                    break;
                case 6:
                    rDate = 30;
                    break;
                case 7:
                    rDate = 31;
                    break;
                case 8:
                    rDate = 31;
                    break;
                case 9:
                    rDate = 30;
                    break;
                case 10:
                    rDate = 31;
                    break;
                case 11:
                    rDate = 30;
                    break;
                case 12:
                    rDate = 31;
                    break;
            }
            return rDate;
        }
        /// <summary>
        /// Returns last date of the given Hijri Month in the given year
        /// </summary>
        internal int LastDateH(int HijriMonth, int HijriYear)
        {
            System.Globalization.HijriCalendar HijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar GregCalendar = new System.Globalization.GregorianCalendar();
            int day = 0, newDay = 0;
            int month, newMonth;
            int year, newYear;
            if (HijriMonth == 12)
            {
                DateTime HijriDateTime = new DateTime(HijriYear + 1, 1, 1, HijriCalendar);
                day = GregCalendar.GetDayOfMonth(HijriDateTime);
                month = GregCalendar.GetMonth(HijriDateTime);
                year = GregCalendar.GetYear(HijriDateTime);
            }
            else
            {
                DateTime HijriDateTime = new DateTime(HijriYear, HijriMonth + 1, 1, HijriCalendar);
                day = GregCalendar.GetDayOfMonth(HijriDateTime);
                month = GregCalendar.GetMonth(HijriDateTime);
                year = GregCalendar.GetYear(HijriDateTime);
            }
            newDay = GetDate_G_Back(day, 1, month, year);
            newMonth = GetMonth_G_Back(month);
            newYear = GetYear_G_Back(year);
            string Gdate = GregToHijriDirect(newDay, newMonth, newYear, false);
            string ourStr = "";
            bool start = false;
            foreach (char ch in Gdate)
            {
                if (ch == '/')
                {
                    start = !start;
                }
                else
                    if (start)
                        ourStr += ch;

            }
            return Int32.Parse(ourStr);
        }

        /// <summary>
        ///  Checks wether the given Hijri date is currect in the given month of the year. otherwise returns today day
        /// </summary>
        internal int CheckDateH(int date, int month, int year)
        {
            if (month > 12 | month < 1) { month = Month(); }
            if (date > LastDateH(month, year) | date < 1)
            {
                HEA.HijriError = "Enter valid date for this month between " + LastDateH(month, year) + " and 1.\nSetting Defult date.";
                SetError(this, HEA);
                return Day();
            }
            else
            {
                return date;
            }
        }
        /// <summary>
        ///  Checks wether the given Hijri month is currect in the given year. otherwise returns today month
        /// </summary>
        internal int CheckMonthH(int month)
        {
            if (month > 12 | month < 1)
            {
                HEA.HijriError = "Enter valid month between 12 and 1.\nSetting Defult month.";
                SetError(this, HEA);
                return Month();
            }
            else
            {
                return month;
            }
        }
        /// <summary>
        ///  Checks wether the given Hijri year is currect. otherwise returns today year
        /// </summary>
        internal int CheckYearH(int year)
        {
            if (year < 1 | year > 9666)
            {
                HEA.HijriError = "Enter valid year between 1 and 9666.\nSetting Defult year.";
                SetError(this, HEA);
                return Year();
            }
            else
            {
                return year;
            }
        }
        /// <summary>
        ///  Checks wether the given Gregorian date is currect in the given month of the year. otherwise returns today day
        /// </summary>
        internal int CheckDateG(int date, int month, int year)
        {
            if (month > 12 | month < 1) { month = DateTime.Now.Month; }
            if (date > LastDateG(month, year) | date < 1)
            {
                HEA.HijriError = "Enter valid date for this month between " + LastDateG(month, year) + " and 1.\nSetting Defult date.";
                SetError(this, HEA);
                return DateTime.Now.Day;
            }
            else
            {
                return date;
            }
        }
        /// <summary>
        ///  Checks wether the given Gregorian month is currect in the given year. otherwise returns today month
        /// </summary>
        internal int CheckMonthG(int month)
        {
            if (month > 12 | month < 1)
            {
                HEA.HijriError = "Enter valid month between 12 and 1.\nSetting Defult month.";
                SetError(this, HEA);
                return DateTime.Now.Month;
            }
            else
            {
                return month;
            }
        }
        /// <summary>
        ///  Checks wether the given Gregorian year is currect. otherwise returns today year
        /// </summary>
        internal int CheckYearG(int year)
        {
            if (year < 1 | year > 9666)
            {
                HEA.HijriError = "Enter valid year between 1 and 9666.\nSetting Defult year.";
                SetError(this, HEA);
                return DateTime.Now.Year;
            }
            else
            {
                return year;
            }
        }

        /// <summary>
        ///  Gives you day from today Hijri date, with effect of dateDifference
        /// </summary>
        internal int Day()
        {
            return DateTime.Parse(GregToHijri(mainDate, mainMonth, mainYear, false)).Day;
        }
        /// <summary>
        ///  Gives you month from today Hijri date, with effect of dateDifference
        /// </summary>
        internal int Month()
        {
            return DateTime.Parse(GregToHijri(mainDate, mainMonth, mainYear, false)).Month;
        }
        /// <summary>
        ///  Gives you year from today Hijri date, with effect of dateDifference
        /// </summary>
        internal int Year()
        {
            return DateTime.Parse(GregToHijri(mainDate, mainMonth, mainYear, false)).Year;
        }

        /// <summary>
        ///  Gives you the starting date of the spacified month of the year.
        /// </summary>
        public string MonthDate(int HijriMonth, int HijriYear, bool MonthName)
        {
            if (MonthName)
            { return HijriToGreg(01, HijriMonth, HijriYear, true); }
            else
            {
                return HijriToGreg(01, HijriMonth, HijriYear, false);
            }
        }

        /// <summary>
        ///  Converts the Gregorian date comprising of day, month and year to Hijri date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string GregToHijri(int GregDay, int GregMonth, int GregYear, bool MonthName)
        {
            GregYear = CheckYearG(GregYear);
            GregMonth = CheckMonthG(GregMonth);
            GregDay = CheckDateG(GregDay, GregMonth, GregYear);

            System.Globalization.HijriCalendar myHijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar myGregorianCalendar = new System.Globalization.GregorianCalendar();

            string changedDate = GetChangedDateG(GregDay, DateDiff, GregMonth, GregYear);

            GregDay = DateTime.Parse(changedDate).Day;
            GregMonth = DateTime.Parse(changedDate).Month;
            GregYear = DateTime.Parse(changedDate).Year;

            DateTime newGreg = new DateTime(GregYear, GregMonth, GregDay, myGregorianCalendar);
            DateTime newLimitG = new DateTime(622, 07, 19, myGregorianCalendar);
            if (newGreg < newLimitG)
            {
                HEA.HijriError = "Hijri Celandar can't go befor 01,01,01. In other words Hijri started July 19, 622";
                SetError(this, HEA);
                return "";
            }
            else
            {
                if (MonthName)
                {
                    return System.Enum.GetName(typeof(HijriMonthNames), myHijriCalendar.GetMonth(newGreg)) + " " + myHijriCalendar.GetDayOfMonth(newGreg) + ",  " + myHijriCalendar.GetYear(newGreg);
                }
                else
                {
                    return myHijriCalendar.GetMonth(newGreg) + "/" + myHijriCalendar.GetDayOfMonth(newGreg) + "/" + myHijriCalendar.GetYear(newGreg);
                }
            }
        }
        /// <summary>
        ///  Converts the Gregorian date in Gregorian DateTime to Hijri date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string GregToHijri(DateTime Gregorian, bool MonthName)
        {
            System.Globalization.HijriCalendar myHijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar myGregorianCalendar = new System.Globalization.GregorianCalendar();
            string changedDate = GetChangedDateG(Gregorian.Day, DateDiff, Gregorian.Month, Gregorian.Year);

            int day = DateTime.Parse(changedDate).Day;
            int month = DateTime.Parse(changedDate).Month;
            int year = DateTime.Parse(changedDate).Year;
            DateTime thisGreg = new DateTime(year, month, day);
            DateTime newLimitG = new DateTime(622, 07, 19, myGregorianCalendar);
            if (thisGreg < newLimitG)
            {
                HEA.HijriError = "Hijri Celandar can't go befor 01,01,01. In other words Hijri started July 19, 622";
                SetError(this, HEA);
                return "";
            }
            else
            {
                if (MonthName)
                {
                    return System.Enum.GetName(typeof(HijriMonthNames), myHijriCalendar.GetMonth(thisGreg)) + " " + myHijriCalendar.GetDayOfMonth(thisGreg) + ",  " + myHijriCalendar.GetYear(thisGreg);
                }
                else
                {
                    return myHijriCalendar.GetMonth(thisGreg) + "/" + myHijriCalendar.GetDayOfMonth(thisGreg) + "/" + myHijriCalendar.GetYear(thisGreg);
                }
            }
        }
        /// <summary>
        ///  Converts the Gregorian date mm_dd_yy(yy) to Hijri date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string GregToHijri(string mm_dd_yyyy, bool MonthName)
        {
            int gregDay = 0;
            int gregMonth = 0;
            int gregYear = 0;
            try
            {
                gregDay = DateTime.Parse(mm_dd_yyyy).Day;
                gregMonth = DateTime.Parse(mm_dd_yyyy).Month;
                gregYear = DateTime.Parse(mm_dd_yyyy).Year;
            }
            catch
            {
                HEA.HijriError = "Date con't be recognised. Enter date as formated mm_dd_yyyy.\nSetting default date.";
                SetError(this, HEA);
                gregDay = DateTime.Now.Day; gregMonth = DateTime.Now.Month; gregYear = DateTime.Now.Year;
            }
            if (MonthName)
                return GregToHijri(gregDay, gregMonth, gregYear, true);
            else
                return GregToHijri(gregDay, gregMonth, gregYear, false);
        }
        /// <summary>
        ///  Converts the Hijri date comprising of day, month and year to Gregorian date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string HijriToGreg(int HijriDay, int HijriMonth, int HijriYear, bool MonthName)
        {
            HijriYear = CheckYearH(HijriYear);
            HijriMonth = CheckMonthH(HijriMonth);
            HijriDay = CheckDateH(HijriDay, HijriMonth, HijriYear);

            System.Globalization.HijriCalendar myHijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar myGregorianCalendar = new System.Globalization.GregorianCalendar();
            string changedDate = GetChangedDateH(HijriDay, DateDiff, HijriMonth, HijriYear);
            HijriDay = dateParse(changedDate);
            HijriMonth = monthParse(changedDate);
            HijriYear = yearParse(changedDate);
            DateTime newHijri = new DateTime(HijriYear, HijriMonth, HijriDay, myHijriCalendar);
            if (MonthName)
            {
                return System.Enum.GetName(typeof(GregMonthNames), myGregorianCalendar.GetMonth(newHijri)) + " " + myGregorianCalendar.GetDayOfMonth(newHijri) + ",  " + myGregorianCalendar.GetYear(newHijri);
            }
            else
            {
                return myGregorianCalendar.GetMonth(newHijri) + "/" + myGregorianCalendar.GetDayOfMonth(newHijri) + "/" + myGregorianCalendar.GetYear(newHijri);
            }
        }
        /// <summary>
        ///  Converts the Hijri date in Hijri DateTime to Gregorian date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string HijriToGreg(DateTime Hijri, bool MonthName)
        {
            System.Globalization.HijriCalendar hijriCal = new System.Globalization.HijriCalendar();
            int hijriDay = hijriCal.GetDayOfMonth(Hijri);
            int hijriMonth = hijriCal.GetMonth(Hijri);
            int hijriYear = hijriCal.GetYear(Hijri);
            return HijriToGreg(hijriMonth + "/" + hijriDay + "/" + hijriYear, false);
        }
        /// <summary>
        ///  Converts the Hijri date mm_dd_yy(yy) to Gregorian date string in mm_dd_yyyy format. MonthName determines showing name in out string.
        /// </summary>
        public string HijriToGreg(string mm_dd_yyyy, bool MonthName)
        {
            int hijriDay = 0;
            int hijriMonth = 0;
            int hijriYear = 0;
            try
            {
                hijriDay = DateTime.Parse(mm_dd_yyyy).Day;
                hijriMonth = DateTime.Parse(mm_dd_yyyy).Month;
                hijriYear = DateTime.Parse(mm_dd_yyyy).Year;
            }
            catch
            {
                HEA.HijriError = "Date con't be recognised. Enter date as formated mm_dd_yyyy.\nSetting default date.";
                SetError(this, HEA);
                hijriDay = Day(); hijriMonth = Month(); hijriYear = Year();
            }
            if (MonthName)
                return HijriToGreg(hijriDay, hijriMonth, hijriYear, true);
            else
                return HijriToGreg(hijriDay, hijriMonth, hijriYear, false);

        }

        /// <summary>
        ///  Converts the Gregorian date comprising of day, month and year to Hijri date string in mm_dd_yyyy format. MonthName determines showing name in out string. The difference to other methods is that it don't concern dateDifference variable.
        /// </summary>
        internal string GregToHijriDirect(int GregDay, int GregMonth, int GregYear, bool MonthName)
        {
            GregYear = CheckYearG(GregYear);
            GregMonth = CheckMonthG(GregMonth);
            GregDay = CheckDateG(GregDay, GregMonth, GregYear);

            System.Globalization.HijriCalendar myHijriCalendar = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar myGregorianCalendar = new System.Globalization.GregorianCalendar();

            //string changedDate = GetChangedDateG(GregDay, DateDiff, GregMonth, GregYear);
            //GregDay = DateTime.Parse(changedDate).Day;
            //GregMonth = DateTime.Parse(changedDate).Month;
            //GregYear = DateTime.Parse(changedDate).Year;

            DateTime newGreg = new DateTime(GregYear, GregMonth, GregDay, myGregorianCalendar);
            DateTime newLimitG = new DateTime(622, 07, 19, myGregorianCalendar);
            if (newGreg < newLimitG)
            {
                HEA.HijriError = "Hijri Celandar can't go befor 01,01,01. In other words Hijri started July 19, 622";
                SetError(this, HEA);
                return "";
            }
            else
            {
                if (MonthName)
                {
                    return System.Enum.GetName(typeof(HijriMonthNames), myHijriCalendar.GetMonth(newGreg)) + " " + myHijriCalendar.GetDayOfMonth(newGreg) + ",  " + myHijriCalendar.GetYear(newGreg);
                }
                else
                {
                    return myHijriCalendar.GetMonth(newGreg) + "/" + myHijriCalendar.GetDayOfMonth(newGreg) + "/" + myHijriCalendar.GetYear(newGreg);
                }
            }
        }

        /// <summary>
        ///  Compares the two dates in Hijri and Gregorian DateTime, and gives true if equal otherwise false.
        /// </summary>
        public bool Equals(DateTime Hijri, DateTime Gregorian)
        {
            string GregOfHijriDate = HijriToGreg(Hijri, false);
            string GregDate = Gregorian.Month + "/" + Gregorian.Day + "/" + Gregorian.Year;
            if (GregDate == GregOfHijriDate)
                return true;
            else
                return false;
        }
        /// <summary>
        ///  Compares the two dates mm_dd_yyyy_Hijri and Gmm_dd_yyyy_Gregorian, and gives true if equal otherwise false.
        /// </summary>
        public bool Equals(string mm_dd_yyyy_Hijri, string mm_dd_yyyy_Gregorian)
        {
            System.Globalization.HijriCalendar hijriCal = new System.Globalization.HijriCalendar();
            DateTime hijriDT = new DateTime(DateTime.Parse(mm_dd_yyyy_Hijri).Year, DateTime.Parse(mm_dd_yyyy_Hijri).Month, DateTime.Parse(mm_dd_yyyy_Hijri).Day, hijriCal);
            return Equals(hijriDT, DateTime.Parse(mm_dd_yyyy_Gregorian));
        }
        /// <summary>
        ///  Compares the two dates in Hijri and Gregorian DateTime, and gives -1 if Hijri is large, 1 if Gregorain is large and 0 if they are equal.
        /// </summary>
        public int Compare(DateTime Hijri, DateTime Gregorian)
        {
            string gregOfHijriDate = HijriToGreg(Hijri, false);
            DateTime gregOfHijriDT = new DateTime(DateTime.Parse(gregOfHijriDate).Year, DateTime.Parse(gregOfHijriDate).Month, DateTime.Parse(gregOfHijriDate).Day);
            if (gregOfHijriDT == Gregorian)
                return 0;
            else
                if (gregOfHijriDT < Gregorian)
                    return 1;
                else
                    return -1;
        }
        /// <summary>
        ///  Compares the two dates mm_dd_yyyy_HIJRI and mm_dd_yyyy_Gregorian, and gives -1 if Hijri is large, 1 if Gregorain is large and 0 if they are equal.
        /// </summary>
        public int Compare(string mm_dd_yyyy_HIJRI, string mm_dd_yyyy_Gregorian)
        {
            System.Globalization.HijriCalendar hijriCal = new System.Globalization.HijriCalendar();
            DateTime hijriDT = new DateTime(DateTime.Parse(mm_dd_yyyy_HIJRI).Year, DateTime.Parse(mm_dd_yyyy_HIJRI).Month, DateTime.Parse(mm_dd_yyyy_HIJRI).Day, hijriCal);
            return Compare(hijriDT, DateTime.Parse(mm_dd_yyyy_Gregorian));

        }
    }
    enum HijriMonthNames
    {
        Moharram = 1, Safer, Rabi_ul_Awal, Rabi_us_Sany, Jmadi_ul_Ula, Jmadi_us_Sany, Rajab, Shaban, Ramzan, Shawal, Zul_Qaida, Zul_Hijja
    };
    enum GregMonthNames
    {
        January = 1, Fabruary, March, April, May, Jun, July, August, September, October, November, December
    };
    public class PrayerTime
    {
        bool fortyHour = false;
        int newHour = 0;
        int date = 1; int month = 1; int year = 1; int diff = 0; int hDate = 1; int hMonth = 1; int hYear = 1; int fDiffA = 0; int zDiffA = 0; int aDiffA = 0; int mDiffA = 0; int eDiffA = 0; int jDiffA = 0; int fDiffN = 0; int zDiffN = 0; int aDiffN = 0; int mDiffN = 0; int eDiffN = 0; int jDiffN = 0;
        /// <summary>
        /// Alarm Variable
        /// </summary>
        bool FajirAlarm; bool ZoharAlarm; bool AserAlarm; bool MaghribAlarm; bool EshaAlarm; bool JummahAlarm; bool AfterAlarm; bool BeforAlarm;
        int FHour, FMinute, ZHour, ZMinute, AHour, AMinute, MHour, MMinute, EHour, EMinute, JHour, JMinute;
        int FAHour, FAMinute, ZAHour, ZAMinute, AAHour, AAMinute, MAHour, MAMinute, EAHour, EAMinute, JAHour, JAMinute, After = 0;
        int FBHour, FBMinute, ZBHour, ZBMinute, ABHour, ABMinute, MBHour, MBMinute, EBHour, EBMinute, JBHour, JBMinute, Befor = 0;
        HijriConversion HConversion = new HijriConversion();
        /// <summary>
        /// EVENT THAT IS RAISED WHEN A ERROR FOUND 
        /// </summary>
        PrayerErrorEventArgs EEA = new PrayerErrorEventArgs("Error! Please view help.");
        public delegate void AnError(object sender, PrayerErrorEventArgs EEA);
        public delegate void Alarm(object sender, PrayerErrorEventArgs EEA);
        public event AnError ErrorFound;
        public event Alarm NamazAlarm;
        /// <summary>
        /// STARTING HERE METHODS PROPERTIES
        /// </summary>
        public PrayerTime()
        {

            date = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            hDate = HConversion.Day();
            hMonth = HConversion.Month();
            hYear = HConversion.Year();
        }
        /// <summary>
        /// PROPERTIES OF PRAYER TIME CLASS TO SET, INPUT OR GET OUTPUT  
        /// </summary>
        public bool FortyHourTime
        {
            set
            {
                fortyHour = value;
            }
            get { return fortyHour; }
        }
        public int DateDifference
        {
            set { HConversion.DateDifference = value; }
        }
        public int Date
        {
            get { return date; }
            set
            {
                if (value > HConversion.LastDateG(month, year) | value < 1)
                {
                    EEA.ErrorMessage = "Enter Currect Date for this Month between " + HConversion.LastDateG(month, year) + " and 1.\nNow setting defult date for Month.";
                    ErrorFound(this, EEA);
                }
                else
                {
                    date = value;
                }
            }
        }
        public int HijriDate
        {
            get { return date; }
            set
            {
                if (value > HConversion.LastDateH(hMonth, hYear) | value < 1)
                {
                    EEA.ErrorMessage = "Enter Currect Date for this Month, Between " + HConversion.LastDateH(hMonth, hYear).ToString() + " and 1.\nNow setting defult date for Month.";
                    ErrorFound(this, EEA);
                }
                else
                {
                    getGDate(value);
                }
            }
        }
        public int Month
        {
            get { return month; }
            set
            {
                if (value > 12 | value < 1)
                {
                    EEA.ErrorMessage = "Enter Current month between 12 and 1.";
                    ErrorFound(this, EEA);
                }
                else
                {
                    month = value;
                }
            }
        }
        public int HijriYear
        {
            get { return hYear; }
            set
            {
                if (value > 1)
                    hYear = value;
            }
        }
        public int PlaceDiff
        {
            get { return diff; }
            set
            {
                if (value < 100 & value > -100) { diff = value; }
                else
                {
                    EEA.ErrorMessage = "Enter Different between 100 and -100.";
                    ErrorFound(this, EEA);
                }
            }
        }
        public string Sadiq
        {
            get { return SSadiq(); }
        }
        public string SunRise
        {
            get { return FajirN(); }
        }
        public string ZoherStart
        {
            get { return ZoherS(); }
        }
        public string ZoherEnd
        {
            get { return ZoherE(); }
        }
        public string Aser
        {
            get { return AserN(); }
        }
        public string Maghrib
        {
            get { return MaghribN(); }
        }
        public string Esha
        {
            get { return EshaN(); }
        }
        /// <summary>
        /// FINAL PRAYER TIME ARE PROVIDED AS STRING
        /// </summary>
        private string SSadiq()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 0], diff).ToString();
            string h = getHour(tArray[0, 0]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string FajirN()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 1], diff).ToString();
            string h = getHour(tArray[0, 1]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string ZoherS()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 2], diff).ToString();
            string h = getHour(tArray[0, 2]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string ZoherE()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 3], diff).ToString();
            string h = getHour(tArray[0, 3]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string AserN()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 4], diff).ToString();
            string h = getHour(tArray[0, 4]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string MaghribN()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 5], diff).ToString();
            string h = getHour(tArray[0, 5]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        private string EshaN()
        {
            int[,] tArray = new int[2, 7];
            tArray = getMonth();
            string m = getMinute(tArray[1, 6], diff).ToString();
            string h = getHour(tArray[0, 6]).ToString();
            if (Int16.Parse(m) < 10)
                m = "0" + m;
            if (Int16.Parse(h) < 10)
                h = "0" + h;
            return h + ":" + m;
        }
        /// <summary>
        /// DIFFERENT ADDED NAMAZ TIMES
        /// </summary>
        private string AFajir()
        {

            int m = Int32.Parse(SSadiq().Substring(3, 2));
            int h = Int32.Parse(SSadiq().Substring(0, 2));
            int fM = getMinute(m, fDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string AZohar()
        {
            int m = Int32.Parse(ZoherS().Substring(3, 2));
            int h = Int32.Parse(ZoherS().Substring(0, 2));
            int fM = getMinute(m, zDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string AAser()
        {
            int m = Int32.Parse(AserN().Substring(3, 2));
            int h = Int32.Parse(AserN().Substring(0, 2));
            int fM = getMinute(m, aDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string AMaghrib()
        {
            int m = Int32.Parse(MaghribN().Substring(3, 2));
            int h = Int32.Parse(MaghribN().Substring(0, 2));
            int fM = getMinute(m, mDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string AEsha()
        {
            int m = Int32.Parse(EshaN().Substring(3, 2));
            int h = Int32.Parse(EshaN().Substring(0, 2));
            int fM = getMinute(m, eDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string AJummah()
        {
            int m = Int32.Parse(ZoherS().Substring(3, 2));
            int h = Int32.Parse(ZoherS().Substring(0, 2));
            int fM = getMinute(m, jDiffA);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        /// <summary>
        /// NAMAZ TIME
        /// </summary>
        private string NFajir()
        {
            int m = Int32.Parse(SSadiq().Substring(3, 2));
            int h = Int32.Parse(SSadiq().Substring(0, 2));
            int fM = getMinute(m, fDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string NZohar()
        {
            int m = Int32.Parse(ZoherS().Substring(3, 2));
            int h = Int32.Parse(ZoherS().Substring(0, 2));
            int fM = getMinute(m, zDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string NAser()
        {
            int m = Int32.Parse(AserN().Substring(3, 2));
            int h = Int32.Parse(AserN().Substring(0, 2));
            int fM = getMinute(m, aDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string NMaghrib()
        {
            int m = Int32.Parse(MaghribN().Substring(3, 2));
            int h = Int32.Parse(MaghribN().Substring(0, 2));
            int fM = getMinute(m, mDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string NEsha()
        {
            int m = Int32.Parse(EshaN().Substring(3, 2));
            int h = Int32.Parse(EshaN().Substring(0, 2));
            int fM = getMinute(m, eDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        private string NJummah()
        {
            int m = Int32.Parse(ZoherS().Substring(3, 2));
            int h = Int32.Parse(ZoherS().Substring(0, 2));
            int fM = getMinute(m, jDiffN);
            int fH = getHour(h);
            string fMs = fM.ToString();
            string fHs = fH.ToString();
            if (Int16.Parse(fMs) < 10)
                fMs = "0" + fMs;
            if (Int16.Parse(fHs) < 10)
                fHs = "0" + fHs;
            return fHs + ":" + fMs;
        }
        /// <summary>
        /// GETTING CURRENT MONTH AND MINUTE HOUR MEHTODS ARE ALSO HERE
        /// </summary>        
        public override string ToString()
        {
            return "Say prayer at time just it is asked. Ahmad Ali.";
        }
        private void getGDate(int hDate)
        {
            string tDate = HConversion.HijriToGreg(hDate, hMonth, hYear, false);
            int rDay = DateTime.Parse(tDate).Day; ;
            int rMonth = DateTime.Parse(tDate).Month;
            this.hDate = hDate;
            month = rMonth;
            date = rDay;
        }
        public void SetHijriMonth(int hYear, int hMonth, int hDate)
        {
            hDate = HConversion.CheckDateH(hDate, hMonth, hYear);
            hMonth = HConversion.CheckMonthG(hMonth);
            hYear = HConversion.CheckYearG(hYear);
            string tDate = HConversion.HijriToGreg(hDate, hMonth, hYear, false);
            int rMonth = Int32.Parse(tDate.Substring(3, 2));
            int rDay = Int32.Parse(tDate.Substring(0, 2));
            this.hMonth = hMonth;
            this.hDate = hDate;
            this.hYear = hYear;
            this.month = rMonth;
            this.date = rDay;
        }
        public void SetAlarms(bool Fajir, bool Zohar, bool Aser, bool Maghrib, bool Esha, bool Jummah, bool After, bool Befor)
        {
            FajirAlarm = Fajir;
            ZoharAlarm = Zohar;
            AserAlarm = Aser;
            MaghribAlarm = Maghrib;
            EshaAlarm = Esha;
            JummahAlarm = Jummah;
            AfterAlarm = After;
            BeforAlarm = Befor;
            fortyHour = true;
            FHour = Int32.Parse(NFajir().Substring(0, 2));
            FMinute = Int32.Parse(NFajir().Substring(3, 2));
            ZHour = Int32.Parse(NZohar().Substring(0, 2));
            ZMinute = Int32.Parse(NZohar().Substring(3, 2));
            AHour = Int32.Parse(NAser().Substring(0, 2));
            AMinute = Int32.Parse(NAser().Substring(3, 2));
            MHour = Int32.Parse(NMaghrib().Substring(0, 2));
            MMinute = Int32.Parse(NMaghrib().Substring(3, 2));
            EHour = Int32.Parse(NEsha().Substring(0, 2));
            EMinute = Int32.Parse(NEsha().Substring(3, 2));
            JHour = Int32.Parse(NJummah().Substring(0, 2));
            JMinute = Int32.Parse(NJummah().Substring(3, 2));

            FAMinute = getMinute(FMinute, this.After);
            FAHour = getHour(FHour);
            ZAMinute = getMinute(ZMinute, this.After);
            ZAHour = getHour(ZHour);
            AAMinute = getMinute(AMinute, this.After);
            AAHour = getHour(AHour);
            MAMinute = getMinute(MMinute, this.After);
            MAHour = getHour(MHour);
            EAMinute = getMinute(EMinute, this.After);
            EAHour = getHour(EHour);
            JAMinute = getMinute(JMinute, this.After);
            JAHour = getHour(JHour);

            FBMinute = getMinute(FMinute, -this.Befor);
            FBHour = getHour(FHour);
            ZBMinute = getMinute(ZMinute, -this.Befor);
            ZBHour = getHour(ZHour);
            ABMinute = getMinute(AMinute, -this.Befor);
            ABHour = getHour(AHour);
            MBMinute = getMinute(MMinute, -this.Befor);
            MBHour = getHour(MHour);
            EBMinute = getMinute(EMinute, -this.Befor);
            EBHour = getHour(EHour);
            JBMinute = getMinute(JMinute, -this.Befor);
            JBHour = getHour(JHour);
            fortyHour = false;
        }
        public void SetAfterBefor(int After, int Befor)
        {
            if (After < 0 | After > 60)
            {
                EEA.ErrorMessage = "Give Value between 60 and 0.";
                ErrorFound(this, EEA);
            }
            else
            { this.After = After; }
            if (Befor < 0 | Befor > 60)
            {
                EEA.ErrorMessage = "Give Value between 60 and 0.";
                ErrorFound(this, EEA);
            }
            else
            { this.Befor = Befor; }
        }
        public void CheckAlarm()
        {
            int hour = DateTime.Now.Hour; int minute = DateTime.Now.Minute;
            int seconds = DateTime.Now.Second; int mSeconds = DateTime.Now.Millisecond;
            if (FajirAlarm == true & hour == FHour & minute == FMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Fajir Namas Time!";
                NamazAlarm(this, EEA);
            }
            if (ZoharAlarm == true & hour == ZHour & minute == ZMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Zohar Namas Time!";
                NamazAlarm(this, EEA);
            }
            if (AserAlarm == true & hour == AHour & minute == AMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Aser Namas Time!";
                NamazAlarm(this, EEA);
            }
            if (MaghribAlarm == true & hour == MHour & minute == MMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Maghrib Namas Time!";
                NamazAlarm(this, EEA);
            }
            if (EshaAlarm == true & hour == EHour & minute == EMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Esha Namas Time!";
                NamazAlarm(this, EEA);
            }
            if (JummahAlarm == true & hour == JHour & minute == JMinute & seconds == 00 & mSeconds < 100)
            {
                EEA.AlarmMessage = "Jummah Namas Time!";
                NamazAlarm(this, EEA);
            }
            /////////////////////////////////////////////
            if (AfterAlarm)
            {
                if (FajirAlarm == true & hour == FAHour & minute == FAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Fajir Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (ZoharAlarm == true & hour == ZAHour & minute == ZAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Zohar Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (AserAlarm == true & hour == AAHour & minute == AAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Aser Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (MaghribAlarm == true & hour == MAHour & minute == MAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Maghrib Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (EshaAlarm == true & hour == EAHour & minute == EAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Esha Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (JummahAlarm == true & hour == JAHour & minute == JAMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Jummah Namas Time!";
                    NamazAlarm(this, EEA);
                }
            }
            if (BeforAlarm)
            {
                if (FajirAlarm == true & hour == FBHour & minute == FBMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Fajir Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (ZoharAlarm == true & hour == ZBHour & minute == ZBMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Zohar Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (AserAlarm == true & hour == ABHour & minute == ABMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Aser Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (MaghribAlarm == true & hour == MBHour & minute == MBMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Maghrib Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (EshaAlarm == true & hour == EBHour & minute == EBMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Esha Namas Time!";
                    NamazAlarm(this, EEA);
                }
                if (JummahAlarm == true & hour == JBHour & minute == JBMinute & seconds == 00 & mSeconds < 100)
                {
                    EEA.AlarmMessage = "Jummah Namas Time!";
                    NamazAlarm(this, EEA);
                }

            }
        }
        public void SetAzanTimes(int Fajir, int Zohar, int Aser, int Maghrib, int Esha, int Jummah)
        {
            if (Fajir > 360 | Fajir < 0)
            {
                EEA.ErrorMessage = "Enter Fijri Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { fDiffA = Fajir; }
            if (Zohar > 360 | Zohar < 0)
            {
                EEA.ErrorMessage = "Enter Zohar Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { zDiffA = Zohar; }
            if (Aser > 360 | Aser < 0)
            {
                EEA.ErrorMessage = "Enter Aser Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { aDiffA = Aser; }
            if (Maghrib > 360 | Maghrib < 0)
            {
                EEA.ErrorMessage = "Enter Maghrib Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { mDiffA = Maghrib; }
            if (Esha > 360 | Esha < 0)
            {
                EEA.ErrorMessage = "Enter Esha Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { eDiffA = Esha; }
            if (Jummah > 360 | Jummah < 0)
            {
                EEA.ErrorMessage = "Enter Jummah Azan Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { jDiffA = Jummah; }
        }
        public
            void SetNamazTimes(int Fajir, int Zohar, int Aser, int Maghrib, int Esha, int Jummah)
        {
            if (Fajir > 360 | Fajir < 0)
            {
                EEA.ErrorMessage = "Enter Fijri Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { fDiffN = Fajir + fDiffA; }
            if (Zohar > 360 | Zohar < 0)
            {
                EEA.ErrorMessage = "Enter Zohar Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { zDiffN = Zohar + zDiffA; }
            if (Aser > 360 | Aser < 0)
            {
                EEA.ErrorMessage = "Enter Aser Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { aDiffN = Aser + aDiffA; }
            if (Maghrib > 360 | Maghrib < 0)
            {
                EEA.ErrorMessage = "Enter Maghrib Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { mDiffN = Maghrib + mDiffA; }
            if (Esha > 360 | Esha < 0)
            {
                EEA.ErrorMessage = "Enter Esha Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { eDiffN = Esha + eDiffA; }
            if (Jummah > 360 | Jummah < 0)
            {
                EEA.ErrorMessage = "Enter Jummah Namaz Difference Between 1 and 360";
                ErrorFound(this, EEA);
            }
            else { jDiffN = Jummah + jDiffA; }

        }
        public String Azan(string AzanName)
        {
            string rStr = "";
            switch (AzanName.ToUpper())
            {
                case "FAJIR":
                    rStr = AFajir();
                    break;
                case "ZOHAR":
                    rStr = AZohar();
                    break;
                case "ASER":
                    rStr = AAser();
                    break;
                case "MAGHRIB":
                    rStr = AMaghrib();
                    break;
                case "ESHA":
                    rStr = AEsha();
                    break;
                case "JUMMAH":
                    rStr = AJummah();
                    break;

            }
            if (rStr == "")
            {
                EEA.ErrorMessage = "Enter Azan name one of these Fajir, Zohar, Aser, Maghrib, Esha or Jummah.";
                ErrorFound(this, EEA);
                return null;
            }
            else
            {
                return rStr;
            }
        }
        public String Namaz(string NamazName)
        {
            string rStr = "";
            switch (NamazName.ToUpper())
            {
                case "FAJIR":
                    rStr = NFajir();
                    break;
                case "ZOHAR":
                    rStr = NZohar();
                    break;
                case "ASER":
                    rStr = NAser();
                    break;
                case "MAGHRIB":
                    rStr = NMaghrib();
                    break;
                case "ESHA":
                    rStr = NEsha();
                    break;
                case "JUMMAH":
                    rStr = NJummah();
                    break;

            }
            if (rStr == "")
            {
                EEA.ErrorMessage = "Enter Namaz name one of these Fajir, Zohar, Aser, Maghrib, Esha or Jummah.";
                ErrorFound(this, EEA);
                return null;
            }
            else
            {
                return rStr;
            }
        }
        private int[,] getMonth()
        {
            int[,] rArray = new int[7, 7];
            switch (month)
            {
                case 1:
                    rArray = january(date);
                    break;
                case 2:
                    rArray = fabruary(date);
                    break;
                case 3:
                    rArray = march(date);
                    break;
                case 4:
                    rArray = april(date);
                    break;
                case 5:
                    rArray = may(date);
                    break;
                case 6:
                    rArray = jun(date);
                    break;
                case 7:
                    rArray = july(date);
                    break;
                case 8:
                    rArray = august(date);
                    break;
                case 9:
                    rArray = september(date);
                    break;
                case 10:
                    rArray = october(date);
                    break;
                case 11:
                    rArray = november(date);
                    break;
                case 12:
                    rArray = december(date);
                    break;
            }
            return rArray;
        }
        public int getMinute(int minute, int diff)
        {
            int rInt = minute;
            if (diff >= 0)
            {
                rInt = minute + diff;
                while (rInt >= 60)
                {
                    rInt -= 60;
                    newHour += 1;
                }
            }
            if (diff < 0)
            {
                rInt = minute + diff;
                if (rInt == 0)
                {
                    newHour -= 1;
                }
                while (rInt < 0)
                {
                    rInt += 60;
                    newHour -= 1;
                }

            }

            return rInt;
        }
        public int getHour(int hour)
        {
            int rInt = 0;
            if (newHour >= 0)
            {
                rInt = hour + newHour;
                newHour = 0;
                if (fortyHour)
                {
                    while (rInt >= 24)
                    {
                        rInt -= 24;
                    }
                }
                else
                {
                    if (rInt >= 12)
                    {
                        rInt -= 12;
                    }
                }
            }
            if (newHour < 0)
            {
                rInt = hour + newHour;
                newHour = 0;
                if (fortyHour)
                {
                    while (rInt < 0)
                    {
                        rInt += 24;
                    }
                }
                else
                {
                    if (rInt < 0)
                    {
                        rInt += 12;
                    }
                }
            }

            return rInt;
        }
        /// <summary>
        /// MAIN DATA OF PRAYER TIMING IN MONTHS, RETURNS 2D ARRAY
        /// </summary>
        private int[,] january(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 36, 3, 7, 39, 32, 10, 37 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 7, 39, 32, 10, 37 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 7, 40, 33, 11, 38 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 8, 40, 33, 11, 38 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 8, 41, 34, 12, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 9, 41, 35, 13, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 9, 42, 36, 14, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 10, 42, 37, 15, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 10, 43, 38, 16, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 4, 11, 43, 39, 17, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 11, 44, 39, 17, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 11, 45, 40, 18, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 12, 45, 41, 19, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 12, 46, 42, 20, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 3, 12, 46, 43, 21, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 3, 12, 46, 44, 22, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 3, 13, 47, 45, 23, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 3, 13, 48, 46, 24, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 3, 13, 49, 47, 25, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 2, 14, 49, 48, 26, 51 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 2, 14, 49, 49, 27, 52 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 37, 1, 14, 50, 50, 28, 53 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 1, 15, 51, 51, 29, 54 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 1, 15, 52, 52, 30, 55 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 0, 15, 53, 53, 30, 56 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 36, 0, 15, 54, 54, 31, 57 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = new int[7] { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 35, 0, 15, 54, 54, 32, 58 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = new int[7] { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 34, 59, 16, 54, 56, 33, 58 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = new int[7] { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 33, 59, 16, 55, 57, 35, 59 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = new int[7] { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = new int[7] { 33, 58, 16, 55, 57, 35, 59 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = new int[7] { 5, 6, 12, 14, 15, 17, 19 };
                int[] mts = new int[7] { 32, 58, 16, 56, 58, 36, 0 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }


            return rArray;

        }
        private int[,] fabruary(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 19 };
                int[] mts = { 32, 56, 16, 56, 59, 37, 0 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 31, 56, 17, 57, 0, 38, 1 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 31, 55, 17, 57, 1, 39, 1 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 30, 54, 17, 58, 2, 40, 2 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 30, 54, 17, 58, 2, 41, 3 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 29, 53, 17, 59, 3, 42, 4 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 7)
            {
                int[] hrs = { 5, 6, 12, 14, 16, 17, 19 };
                int[] mts = { 29, 53, 17, 59, 4, 43, 5 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 28, 52, 17, 0, 4, 44, 5 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 28, 51, 17, 0, 5, 44, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 27, 50, 17, 1, 6, 45, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 11)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 27, 49, 17, 1, 7, 46, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 26, 48, 17, 2, 8, 47, 8 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 13)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 26, 47, 17, 2, 9, 48, 9 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 25, 46, 17, 3, 10, 49, 9 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 15)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 24, 45, 17, 3, 10, 50, 10 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 23, 44, 17, 4, 11, 50, 11 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 22, 43, 17, 4, 12, 51, 12 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 21, 42, 17, 5, 12, 52, 13 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 20, 41, 17, 5, 13, 53, 14 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 19, 40, 17, 6, 14, 54, 14 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 18, 39, 17, 6, 15, 55, 15 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 17, 38, 17, 7, 16, 55, 15 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 16, 37, 16, 7, 16, 56, 16 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 15, 36, 16, 8, 17, 57, 17 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 14, 35, 16, 8, 18, 58, 18 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 13, 34, 16, 8, 19, 58, 18 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 12, 33, 16, 9, 19, 58, 19 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 28)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 11, 32, 16, 9, 20, 59, 20 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 29)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 17, 19 };
                int[] mts = { 11, 32, 16, 9, 20, 59, 20 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] march(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 10, 31, 16, 9, 20, 0, 21 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 9, 29, 15, 9, 21, 0, 22 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 8, 28, 15, 9, 21, 1, 23 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 7, 27, 15, 9, 22, 2, 24 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 5)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 6, 26, 15, 9, 23, 3, 24 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 5, 25, 14, 10, 23, 3, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 7)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 4, 24, 14, 10, 24, 4, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 3, 23, 14, 10, 25, 5, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 2, 22, 14, 10, 25, 6, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 5, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 0, 21, 14, 10, 26, 7, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 11)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 59, 19, 13, 11, 27, 7, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 58, 18, 13, 11, 28, 8, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 56, 17, 13, 11, 28, 9, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 56, 16, 13, 11, 28, 10, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 54, 14, 12, 11, 29, 10, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 53, 13, 12, 11, 29, 11, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 52, 12, 11, 12, 29, 12, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 51, 10, 11, 12, 30, 12, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 50, 9, 11, 12, 30, 13, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 48, 8, 11, 12, 30, 14, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 47, 7, 10, 12, 31, 14, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 46, 6, 10, 12, 31, 15, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 44, 5, 10, 13, 31, 16, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 43, 3, 9, 13, 32, 16, 37 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 41, 2, 9, 13, 33, 17, 37 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 39, 1, 9, 13, 33, 18, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 4, 6, 12, 15, 16, 18, 19 };
                int[] mts = { 38, 0, 9, 13, 33, 18, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 38, 58, 8, 13, 33, 19, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 38, 57, 8, 13, 34, 20, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 35, 56, 8, 13, 34, 20, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 33, 54, 7, 14, 34, 21, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] april(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 32, 52, 7, 14, 34, 21, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 30, 51, 7, 14, 25, 22, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 29, 49, 6, 14, 35, 23, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 27, 48, 6, 14, 35, 23, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 26, 47, 6, 14, 36, 24, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 24, 46, 6, 15, 37, 25, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 23, 45, 5, 15, 37, 26, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 21, 43, 5, 15, 37, 26, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 19, 42, 5, 15, 38, 27, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 18, 41, 4, 16, 38, 28, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 16, 39, 4, 16, 38, 28, 51 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 15, 38, 4, 16, 39, 29, 52 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 13, 37, 4, 16, 39, 29, 53 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 12, 36, 3, 16, 40, 30, 54 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 10, 35, 3, 16, 40, 30, 55 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 9, 34, 3, 16, 41, 31, 56 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 8, 33, 2, 17, 41, 32, 57 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 18)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 6, 32, 2, 17, 42, 32, 59 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 5, 31, 2, 17, 42, 33, 0 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 4, 30, 2, 17, 43, 34, 1 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }

            if (d == 21)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 2, 29, 2, 17, 44, 34, 2 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 1, 28, 2, 17, 44, 35, 3 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 59, 26, 1, 18, 44, 36, 4 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 58, 25, 1, 18, 45, 36, 5 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 57, 24, 1, 18, 46, 37, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 55, 23, 1, 18, 47, 38, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 54, 22, 1, 18, 47, 38, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 53, 21, 1, 18, 48, 39, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 52, 20, 0, 19, 49, 40, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 51, 19, 0, 19, 49, 41, 8 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] may(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 50, 18, 0, 19, 50, 41, 9 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 49, 17, 0, 20, 50, 42, 10 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 48, 16, 0, 20, 51, 42, 11 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 47, 15, 0, 21, 52, 43, 12 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 46, 15, 0, 21, 52, 44, 13 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 45, 14, 0, 22, 53, 45, 14 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 43, 13, 0, 22, 53, 45, 15 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 42, 12, 59, 22, 53, 46, 16 };
                for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 41, 11, 59, 23, 53, 47, 16 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 40, 10, 59, 23, 53, 48, 17 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 38, 9, 59, 23, 53, 48, 18 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 37, 9, 59, 24, 53, 49, 20 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 36, 8, 59, 24, 54, 50, 21 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 35, 7, 59, 24, 54, 50, 22 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 34, 7, 59, 25, 54, 51, 22 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 33, 6, 59, 25, 54, 52, 23 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 33, 5, 59, 25, 54, 52, 24 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 32, 5, 59, 26, 54, 53, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 31, 4, 59, 26, 55, 54, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 30, 3, 59, 26, 55, 54, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 29, 3, 59, 26, 55, 55, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 3, 5, 11, 15, 16, 18, 20 };
                int[] mts = { 28, 3, 59, 27, 55, 56, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 27, 3, 0, 27, 55, 57, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 26, 2, 0, 27, 55, 57, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 26, 1, 0, 28, 56, 58, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 25, 1, 0, 28, 56, 59, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 24, 1, 0, 28, 56, 0, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 23, 0, 0, 29, 56, 0, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 23, 0, 0, 29, 56, 0, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 22, 59, 0, 30, 56, 1, 37 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 22, 59, 0, 30, 56, 1, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] jun(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 21, 59, 1, 30, 57, 2, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 21, 59, 1, 31, 57, 3, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 58, 1, 31, 57, 3, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 58, 1, 31, 57, 3, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 58, 1, 32, 57, 4, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 57, 1, 32, 58, 4, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 57, 2, 32, 58, 5, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 57, 2, 32, 58, 5, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 20, 57, 2, 33, 58, 5, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 2, 33, 58, 6, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 3, 33, 59, 6, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 3, 34, 59, 7, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 3, 34, 59, 7, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 3, 34, 59, 7, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 3, 4, 12, 15, 16, 19, 20 };
                int[] mts = { 19, 57, 3, 35, 59, 8, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 57, 3, 35, 0, 8, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 58, 4, 35, 0, 8, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 58, 4, 36, 0, 9, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 58, 4, 36, 0, 9, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 58, 4, 36, 0, 9, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 58, 4, 37, 1, 9, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 59, 5, 37, 1, 9, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 59, 5, 37, 1, 10, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 3, 4, 12, 15, 17, 19, 20 };
                int[] mts = { 19, 59, 5, 37, 1, 10, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 5, 37, 1, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 6, 38, 1, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 6, 38, 1, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 6, 38, 2, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 6, 38, 2, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 20, 0, 6, 38, 2, 11, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] july(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 22, 1, 6, 39, 2, 11, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 23, 1, 7, 39, 2, 11, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 23, 2, 7, 39, 2, 11, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 24, 3, 7, 39, 2, 11, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 24, 3, 7, 39, 2, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 25, 3, 7, 38, 2, 11, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 26, 4, 7, 38, 2, 11, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 27, 4, 7, 38, 2, 11, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 27, 5, 7, 38, 2, 11, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 28, 5, 7, 38, 2, 10, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 28, 5, 7, 37, 2, 10, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 29, 6, 7, 37, 1, 10, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 29, 6, 7, 37, 1, 10, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 30, 7, 8, 37, 1, 9, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 31, 8, 8, 37, 0, 9, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 32, 9, 9, 37, 0, 9, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 33, 9, 9, 36, 0, 9, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 34, 10, 9, 36, 0, 8, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 35, 10, 9, 36, 0, 8, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 35, 11, 9, 36, 0, 8, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 3, 5, 12, 15, 17, 19, 20 };
                int[] mts = { 36, 11, 9, 36, 0, 7, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 37, 11, 9, 36, 59, 6, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 37, 12, 9, 36, 59, 6, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 38, 13, 9, 36, 59, 5, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 39, 14, 9, 36, 59, 5, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 40, 15, 9, 36, 59, 4, 37 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 41, 16, 9, 35, 59, 3, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 42, 17, 9, 35, 58, 2, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 43, 17, 9, 35, 58, 2, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 44, 18, 9, 35, 58, 1, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 45, 18, 9, 34, 58, 0, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] august(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 19, 20 };
                int[] mts = { 46, 19, 9, 34, 58, 0, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 49, 19, 9, 34, 57, 59, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 47, 20, 9, 33, 57, 58, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 48, 21, 9, 32, 56, 57, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 49, 22, 9, 32, 56, 56, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 50, 23, 9, 31, 55, 55, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 51, 23, 9, 30, 54, 54, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 52, 24, 9, 30, 54, 53, 24 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 53, 24, 8, 29, 53, 52, 23 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 54, 25, 8, 28, 52, 51, 22 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 55, 26, 8, 27, 52, 50, 21 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 56, 27, 8, 27, 51, 49, 20 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 57, 28, 8, 26, 50, 48, 18 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 58, 28, 8, 26, 50, 47, 17 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 58, 29, 7, 25, 49, 47, 16 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 3, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 59, 29, 7, 25, 49, 46, 14 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 0, 30, 7, 24, 48, 45, 13 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 1, 30, 7, 23, 48, 44, 12 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 2, 31, 7, 23, 47, 43, 10 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 3, 31, 6, 22, 46, 42, 9 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 4, 32, 6, 21, 45, 40, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 5, 33, 6, 21, 45, 39, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 6, 34, 6, 20, 44, 38, 4 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 7, 34, 5, 20, 44, 37, 3 };
            }
            if (d == 25)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 8, 35, 5, 20, 43, 36, 1 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 20 };
                int[] mts = { 9, 35, 5, 19, 42, 35, 0 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 10, 35, 5, 18, 41, 34, 58 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 10, 36, 4, 18, 41, 33, 57 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 11, 36, 4, 17, 41, 31, 56 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 12, 37, 4, 17, 40, 30, 55 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 13, 37, 3, 16, 39, 29, 53 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] september(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 14, 37, 3, 16, 38, 28, 52 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 15, 38, 3, 16, 37, 27, 51 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 15, 39, 3, 15, 36, 26, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 16, 39, 2, 15, 35, 24, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 17, 40, 2, 14, 34, 23, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 18, 41, 1, 13, 33, 22, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 18, 42, 1, 13, 32, 20, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 19, 43, 0, 12, 31, 19, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 20, 43, 0, 11, 30, 18, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 21, 44, 0, 10, 29, 16, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 4, 5, 12, 15, 16, 18, 19 };
                int[] mts = { 22, 44, 0, 9, 28, 15, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 23, 45, 59, 8, 27, 14, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 23, 45, 59, 7, 26, 13, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 24, 46, 59, 6, 25, 11, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 24, 46, 58, 5, 24, 10, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 25, 47, 58, 4, 23, 8, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 26, 48, 58, 3, 22, 7, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 26, 49, 57, 2, 21, 6, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 27, 50, 57, 1, 20, 5, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 4, 5, 11, 15, 16, 18, 19 };
                int[] mts = { 28, 51, 56, 0, 19, 4, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 18, 19 };
                int[] mts = { 29, 51, 56, 59, 18, 2, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 18, 19 };
                int[] mts = { 30, 52, 56, 58, 17, 1, 23 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 30, 52, 55, 57, 16, 59, 21 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 31, 53, 55, 56, 15, 58, 20 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 32, 53, 55, 55, 14, 57, 19 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 32, 53, 54, 55, 13, 55, 17 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 33, 54, 54, 54, 12, 54, 16 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 33, 55, 54, 53, 11, 53, 14 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 34, 55, 53, 52, 10, 51, 13 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 34, 56, 53, 51, 9, 50, 11 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] october(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 35, 57, 53, 51, 8, 49, 10 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 36, 57, 53, 51, 7, 48, 8 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 36, 58, 53, 51, 6, 47, 7 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 37, 58, 52, 50, 5, 45, 6 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 4, 5, 11, 14, 16, 17, 19 };
                int[] mts = { 37, 59, 51, 50, 4, 44, 4 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 4, 6, 11, 14, 16, 17, 19 };
                int[] mts = { 38, 0, 51, 50, 3, 43, 3 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 4, 6, 11, 14, 16, 17, 19 };
                int[] mts = { 38, 0, 50, 49, 2, 41, 2 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 4, 6, 11, 14, 16, 17, 19 };
                int[] mts = { 39, 1, 50, 49, 1, 40, 0 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 4, 6, 11, 14, 16, 17, 18 };
                int[] mts = { 40, 1, 50, 48, 0, 39, 59 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 40, 2, 49, 47, 59, 37, 58 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 41, 3, 49, 47, 58, 36, 57 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 42, 3, 49, 46, 57, 35, 56 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 43, 4, 49, 46, 56, 34, 55 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 44, 5, 49, 45, 55, 32, 53 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 44, 5, 49, 44, 54, 31, 51 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 45, 6, 49, 43, 53, 29, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 45, 7, 48, 42, 52, 28, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 46, 8, 48, 41, 51, 27, 50 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 46, 9, 48, 40, 50, 26, 49 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 47, 10, 48, 39, 48, 25, 48 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 48, 11, 48, 38, 47, 24, 47 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 48, 11, 47, 38, 46, 23, 46 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 49, 12, 47, 37, 45, 22, 45 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 50, 13, 47, 37, 44, 21, 44 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 50, 14, 47, 36, 43, 20, 43 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 51, 15, 47, 35, 42, 19, 42 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 52, 15, 47, 34, 41, 18, 41 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 52, 16, 47, 33, 40, 18, 40 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 53, 17, 47, 33, 39, 17, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 54, 18, 47, 32, 39, 16, 39 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 55, 18, 47, 31, 38, 15, 38 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] november(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 56, 19, 47, 31, 37, 14, 37 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 57, 20, 47, 31, 36, 14, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 57, 21, 47, 31, 36, 13, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 58, 22, 47, 31, 35, 12, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 59, 23, 47, 31, 34, 11, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 4, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 59, 23, 47, 29, 34, 10, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 0, 24, 47, 29, 33, 10, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 1, 25, 47, 29, 33, 9, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 1, 25, 47, 29, 32, 9, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 2, 26, 47, 29, 32, 8, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 3, 27, 47, 28, 31, 7, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 3, 28, 47, 28, 30, 6, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 4, 29, 47, 28, 29, 5, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 5, 29, 47, 28, 29, 5, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 5, 30, 47, 28, 28, 4, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 6, 31, 48, 28, 27, 4, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 7, 32, 48, 27, 27, 3, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 8, 33, 48, 27, 26, 3, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 9, 34, 48, 27, 26, 2, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 10, 35, 49, 27, 25, 2, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 10, 36, 49, 27, 25, 2, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 11, 36, 50, 27, 25, 1, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 12, 37, 50, 26, 25, 1, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 13, 38, 51, 26, 24, 0, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 13, 39, 51, 26, 24, 0, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 14, 40, 51, 26, 24, 0, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 15, 40, 51, 26, 24, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 15, 41, 51, 26, 23, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 16, 42, 52, 26, 23, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 17, 43, 52, 27, 23, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
        private int[,] december(int dayNumber)
        {
            int[,] rArray = new int[2, 7];
            int d = dayNumber;
            if (d == 1)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 18, 44, 52, 29, 22, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 2)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 19, 45, 52, 29, 22, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 3)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 20, 46, 53, 29, 22, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 4)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 20, 47, 53, 29, 22, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 5)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 21, 48, 54, 29, 22, 59, 25 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 6)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 22, 49, 54, 30, 23, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 7)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 23, 50, 54, 30, 23, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 8)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 23, 50, 55, 31, 23, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 9)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 24, 51, 56, 31, 23, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 10)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 24, 51, 57, 31, 23, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 11)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 25, 51, 57, 32, 24, 59, 26 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 12)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 16, 18 };
                int[] mts = { 26, 52, 58, 32, 24, 59, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 13)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 26, 53, 58, 32, 24, 0, 27 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 14)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 27, 53, 58, 32, 24, 0, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 15)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 27, 54, 59, 33, 25, 1, 28 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 16)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 28, 55, 59, 33, 25, 1, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 17)
            {
                int[] hrs = { 5, 6, 11, 14, 15, 17, 18 };
                int[] mts = { 29, 56, 59, 33, 26, 2, 29 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 18)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 30, 57, 0, 33, 26, 2, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 19)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 30, 58, 0, 33, 26, 3, 30 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 20)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 31, 58, 1, 33, 27, 4, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 21)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 31, 59, 1, 33, 27, 4, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 22)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 32, 59, 2, 33, 27, 5, 31 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 23)
            {
                int[] hrs = { 5, 6, 12, 14, 15, 17, 18 };
                int[] mts = { 33, 59, 2, 33, 28, 5, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 24)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 33, 0, 3, 34, 28, 6, 32 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 25)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 34, 0, 3, 35, 28, 6, 33 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 26)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 35, 1, 4, 35, 29, 7, 34 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 27)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 35, 1, 4, 35, 29, 7, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 28)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 35, 2, 5, 36, 30, 8, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 29)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 35, 2, 5, 36, 30, 8, 35 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 30)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 36, 3, 6, 36, 31, 9, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            if (d == 31)
            {
                int[] hrs = { 5, 7, 12, 14, 15, 17, 18 };
                int[] mts = { 36, 3, 6, 37, 31, 9, 36 }; for (int i = 0; i < 7; i++)
                {
                    rArray[0, i] = hrs[i];
                }
                for (int i = 0; i < 7; i++)
                {
                    rArray[1, i] = mts[i];
                }
            }
            return rArray;
        }
    }
    public class PrayerErrorEventArgs : EventArgs
    {
        private string message;
        private string alarmMessage;
        public PrayerErrorEventArgs(string message)
        {
            this.message = message;
        }
        public string ErrorMessage
        {
            get { return message; }
            set { message = value; }
        }
        public string AlarmMessage
        {
            get { return alarmMessage; }
            set { alarmMessage = value; }
        }

    }
    public class HijriErrorEventArgs : EventArgs
    {
        private string HijriDateTimeMessage;
        private string HijriDateTimeErr;
        public HijriErrorEventArgs(string message)
        {
            this.HijriDateTimeMessage = message;
        }
        public string EventMessage
        {
            get { return HijriDateTimeMessage; }
            set { HijriDateTimeMessage = value; }
        }
        public string HijriError
        {
            get { return HijriDateTimeErr; }
            set { HijriDateTimeErr = value; }
        }
    }
}

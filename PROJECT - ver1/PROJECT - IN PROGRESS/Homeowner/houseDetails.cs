using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;


namespace PROJECT___IN_PROGRESS
{
    public class bDate
    {
        public DateTime rentStart;
        public DateTime rentEnd;
    }
    
    public class houseDetails
    {
        
        
        public bool bookingCheck(List<bDate> bDates, DateTime startDate, DateTime endDate)
        {
            if (bDates.Count > 0)
            {
                foreach (bDate bD in bDates)
                {
                    if (startDate >= bD.rentStart && startDate >= bD.rentEnd  || startDate <= bD.rentEnd && startDate <= bD.rentStart)
                    {
                        return true;
                    }
                }
            }
            else { return true; }
            return false;
        }
        
        public List<bDate> bookingDate = new List<bDate>();
        public int rentRate;
        public String city;
        public String details;
        public String address;
        public int houseserial;
        public String homeownerid;
        public String houseid;
        public Image image1;
        public Image image2;
        public Image image3;

    }
}

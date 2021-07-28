using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TripAverage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string textFile = "..\\..\\..\\input.txt";
            // Read a text file line by line.  
            string[] lines = File.ReadAllLines(textFile);
            List<Driver> driverList = new List<Driver>();
            List<Trip> tripList = new List<Trip>();

            foreach (string line in lines)
            {
                if(line.ToLower().Contains("driver"))
                {
                    string[] driverArr = line.Split(' ');
                    driverList.Add(new Driver { Name = driverArr[1] });
                }
                else if (line.ToLower().Contains("trip"))
                {
                    string[] tripArr = line.Split(' ');
                    tripList.Add(new Trip { Driver = new Driver { Name = tripArr[1] },StarTime=Convert.ToDateTime(tripArr[2]),EndTime= Convert.ToDateTime(tripArr[3]), Distance=Convert.ToDecimal(tripArr[4])});
                }
            }

            //Report Calculation
            List<Report> reportList = new List<Report>();
            for(int i=0;i<driverList.Count;i++)
            {
                decimal totalDistance = 0, mph=0;
                int counter = 0;
                for (int j=0;j<tripList.Count;j++)
                {
                    if(driverList[i].Name ==tripList[j].Driver.Name)
                    {                        
                        decimal milePerHour= CalculateMilesPerHour((TimeSpan)(tripList[j].EndTime - tripList[j].StarTime), tripList[j].Distance);
                        if(milePerHour>=5 || milePerHour<=100)
                        {
                            counter++;
                            totalDistance += tripList[j].Distance;
                            mph += milePerHour;
                        }
                    }
                }

                int avgMiles = 0;

                if(counter!=0)
                {
                    avgMiles=Convert.ToInt32(mph / counter);
                }

                reportList.Add(new Report { Name = driverList[i].Name, Distance = Convert.ToInt32(totalDistance), mhp = avgMiles });
                
            }

            reportList = reportList.OrderByDescending(x => x.Distance).ToList();
            //Print Report
            for (int i=0;i<reportList.Count;i++)
            {
                Console.WriteLine(reportList[i].Name + ": " + reportList[i].Distance.ToString() + " miles @ " + reportList[i].mhp.ToString()+" mph");
            }
        }

        public static decimal CalculateMilesPerHour(TimeSpan time, decimal distance)
        {
            int minutes = time.Hours*60+ time.Minutes;
            return distance * 60 / minutes;
        }
    }
}

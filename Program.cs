﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class PublicHoliday
{
    public DateTime Date { get; set; }
    public string LocalName { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public bool Global { get; set; }
    // Other properties as needed
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, ToniAnn! Let's calculate some time periods!");
        Console.WriteLine();

        Console.Write("Enter the first date (MM/DD/YYYY): ");
        DateTime firstDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter the time of the first date (HH:mm:ss): ");
        TimeSpan firstTime = TimeSpan.Parse(Console.ReadLine());

        Console.Write("Enter the second date (MM/DD/YYYY): ");
        DateTime secondDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter the time of the second date (HH:mm:ss): ");
        TimeSpan secondTime = TimeSpan.Parse(Console.ReadLine());

        List<PublicHoliday> publicHolidays = LoadPublicHolidaysFromJson(); // Load public holidays from JSON

        TimeSpan timeBetweenDates = secondDate + secondTime - (firstDate + firstTime);
        int calendarDays = timeBetweenDates.Days;
        double totalHours = timeBetweenDates.TotalHours;
        int businessDays = 0;

        for (DateTime date = firstDate; date <= secondDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !IsPublicHoliday(date, publicHolidays))
                businessDays++;
        }

        if (firstTime > TimeSpan.FromHours(17))
            businessDays--;
        if (secondTime < TimeSpan.FromHours(8))
            businessDays--;

        Console.WriteLine();
        Console.WriteLine("Calendar days between {0} {1} and {2} {3} is {4}", firstDate.ToShortDateString(), firstTime.ToString(), secondDate.ToShortDateString(), secondTime.ToString(), calendarDays);
        Console.WriteLine("Business days between {0} {1} and {2} {3} is {4}", firstDate.ToShortDateString(), firstTime.ToString(), secondDate.ToShortDateString(), secondTime.ToString(), --businessDays);
        Console.WriteLine("Total hours between {0} {1} and {2} {3} is {4}", firstDate.ToShortDateString(), firstTime.ToString(), secondDate.ToShortDateString(), secondTime.ToString(), totalHours);
        Console.WriteLine();
        Console.Write("Would you like to calculate another set of dates? (y/n): ");

        //Ask user if they would like to calculate another set of dates
        string answer = Console.ReadLine();
        if (answer.ToLower() == "y")
        {
            Main(args);
        } else if (answer.ToLower() == "n")
        {
            Console.WriteLine("Goodbye!");
        }

        Console.ReadKey(true);
    }

    static List<PublicHoliday> LoadPublicHolidaysFromJson()
    {
        string json = @"[
            {
        ""date"": ""2023-01-02"",
        ""localName"": ""New Year's Day"",
        ""name"": ""New Year's Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-01-16"",
        ""localName"": ""Martin Luther King, Jr. Day"",
        ""name"": ""Martin Luther King, Jr. Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-02-20"",
        ""localName"": ""Presidents Day"",
        ""name"": ""Washington's Birthday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-04-07"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-CT"",
            ""US-DE"",
            ""US-HI"",
            ""US-IN"",
            ""US-KY"",
            ""US-LA"",
            ""US-NC"",
            ""US-ND"",
            ""US-NJ"",
            ""US-TN""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-04-07"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-TX""
        ],
        ""launchYear"": null,
        ""type"": ""Optional""
    },
    {
        ""date"": ""2023-05-29"",
        ""localName"": ""Memorial Day"",
        ""name"": ""Memorial Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-06-19"",
        ""localName"": ""Juneteenth"",
        ""name"": ""Juneteenth"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 2021,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-07-04"",
        ""localName"": ""Independence Day"",
        ""name"": ""Independence Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-09-04"",
        ""localName"": ""Labor Day"",
        ""name"": ""Labour Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-10-09"",
        ""localName"": ""Columbus Day"",
        ""name"": ""Columbus Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-AL"",
            ""US-AZ"",
            ""US-CO"",
            ""US-CT"",
            ""US-GA"",
            ""US-ID"",
            ""US-IL"",
            ""US-IN"",
            ""US-IA"",
            ""US-KS"",
            ""US-KY"",
            ""US-LA"",
            ""US-ME"",
            ""US-MD"",
            ""US-MA"",
            ""US-MS"",
            ""US-MO"",
            ""US-MT"",
            ""US-NE"",
            ""US-NH"",
            ""US-NJ"",
            ""US-NM"",
            ""US-NY"",
            ""US-NC"",
            ""US-OH"",
            ""US-OK"",
            ""US-PA"",
            ""US-RI"",
            ""US-SC"",
            ""US-TN"",
            ""US-UT"",
            ""US-VA"",
            ""US-WV""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-11-10"",
        ""localName"": ""Veterans Day"",
        ""name"": ""Veterans Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-11-23"",
        ""localName"": ""Thanksgiving Day"",
        ""name"": ""Thanksgiving Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 1863,
        ""type"": ""Public""
    },
    {
        ""date"": ""2023-12-25"",
        ""localName"": ""Christmas Day"",
        ""name"": ""Christmas Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-12-31"",
        ""localName"": ""New Year's Day"",
        ""name"": ""New Year's Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-01-17"",
        ""localName"": ""Martin Luther King, Jr. Day"",
        ""name"": ""Martin Luther King, Jr. Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-02-21"",
        ""localName"": ""Presidents Day"",
        ""name"": ""Washington's Birthday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-04-15"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-CT"",
            ""US-DE"",
            ""US-HI"",
            ""US-IN"",
            ""US-KY"",
            ""US-LA"",
            ""US-NC"",
            ""US-ND"",
            ""US-NJ"",
            ""US-TN""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-04-15"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-TX""
        ],
        ""launchYear"": null,
        ""type"": ""Optional""
    },
    {
        ""date"": ""2022-05-30"",
        ""localName"": ""Memorial Day"",
        ""name"": ""Memorial Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-06-20"",
        ""localName"": ""Juneteenth"",
        ""name"": ""Juneteenth"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 2021,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-07-04"",
        ""localName"": ""Independence Day"",
        ""name"": ""Independence Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-09-05"",
        ""localName"": ""Labor Day"",
        ""name"": ""Labour Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-10-10"",
        ""localName"": ""Columbus Day"",
        ""name"": ""Columbus Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-AL"",
            ""US-AZ"",
            ""US-CO"",
            ""US-CT"",
            ""US-GA"",
            ""US-ID"",
            ""US-IL"",
            ""US-IN"",
            ""US-IA"",
            ""US-KS"",
            ""US-KY"",
            ""US-LA"",
            ""US-ME"",
            ""US-MD"",
            ""US-MA"",
            ""US-MS"",
            ""US-MO"",
            ""US-MT"",
            ""US-NE"",
            ""US-NH"",
            ""US-NJ"",
            ""US-NM"",
            ""US-NY"",
            ""US-NC"",
            ""US-OH"",
            ""US-OK"",
            ""US-PA"",
            ""US-RI"",
            ""US-SC"",
            ""US-TN"",
            ""US-UT"",
            ""US-VA"",
            ""US-WV""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-11-11"",
        ""localName"": ""Veterans Day"",
        ""name"": ""Veterans Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-11-24"",
        ""localName"": ""Thanksgiving Day"",
        ""name"": ""Thanksgiving Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 1863,
        ""type"": ""Public""
    },
    {
        ""date"": ""2022-12-26"",
        ""localName"": ""Christmas Day"",
        ""name"": ""Christmas Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-01-01"",
        ""localName"": ""New Year's Day"",
        ""name"": ""New Year's Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-01-18"",
        ""localName"": ""Martin Luther King, Jr. Day"",
        ""name"": ""Martin Luther King, Jr. Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-02-15"",
        ""localName"": ""Presidents Day"",
        ""name"": ""Washington's Birthday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-04-02"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-CT"",
            ""US-DE"",
            ""US-HI"",
            ""US-IN"",
            ""US-KY"",
            ""US-LA"",
            ""US-NC"",
            ""US-ND"",
            ""US-NJ"",
            ""US-TN""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-04-02"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-TX""
        ],
        ""launchYear"": null,
        ""type"": ""Optional""
    },
    {
        ""date"": ""2021-05-31"",
        ""localName"": ""Memorial Day"",
        ""name"": ""Memorial Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-06-18"",
        ""localName"": ""Juneteenth"",
        ""name"": ""Juneteenth"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 2021,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-07-05"",
        ""localName"": ""Independence Day"",
        ""name"": ""Independence Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-09-06"",
        ""localName"": ""Labor Day"",
        ""name"": ""Labour Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-10-11"",
        ""localName"": ""Columbus Day"",
        ""name"": ""Columbus Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-AL"",
            ""US-AZ"",
            ""US-CO"",
            ""US-CT"",
            ""US-GA"",
            ""US-ID"",
            ""US-IL"",
            ""US-IN"",
            ""US-IA"",
            ""US-KS"",
            ""US-KY"",
            ""US-LA"",
            ""US-ME"",
            ""US-MD"",
            ""US-MA"",
            ""US-MS"",
            ""US-MO"",
            ""US-MT"",
            ""US-NE"",
            ""US-NH"",
            ""US-NJ"",
            ""US-NM"",
            ""US-NY"",
            ""US-NC"",
            ""US-OH"",
            ""US-OK"",
            ""US-PA"",
            ""US-RI"",
            ""US-SC"",
            ""US-TN"",
            ""US-UT"",
            ""US-VA"",
            ""US-WV""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-11-11"",
        ""localName"": ""Veterans Day"",
        ""name"": ""Veterans Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-11-25"",
        ""localName"": ""Thanksgiving Day"",
        ""name"": ""Thanksgiving Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 1863,
        ""type"": ""Public""
    },
    {
        ""date"": ""2021-12-24"",
        ""localName"": ""Christmas Day"",
        ""name"": ""Christmas Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-01-01"",
        ""localName"": ""New Year's Day"",
        ""name"": ""New Year's Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-01-15"",
        ""localName"": ""Martin Luther King, Jr. Day"",
        ""name"": ""Martin Luther King, Jr. Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-02-19"",
        ""localName"": ""Presidents Day"",
        ""name"": ""Washington's Birthday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-03-29"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-CT"",
            ""US-DE"",
            ""US-HI"",
            ""US-IN"",
            ""US-KY"",
            ""US-LA"",
            ""US-NC"",
            ""US-ND"",
            ""US-NJ"",
            ""US-TN""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-03-29"",
        ""localName"": ""Good Friday"",
        ""name"": ""Good Friday"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-TX""
        ],
        ""launchYear"": null,
        ""type"": ""Optional""
    },
    {
        ""date"": ""2024-05-27"",
        ""localName"": ""Memorial Day"",
        ""name"": ""Memorial Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-06-19"",
        ""localName"": ""Juneteenth"",
        ""name"": ""Juneteenth"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 2021,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-07-04"",
        ""localName"": ""Independence Day"",
        ""name"": ""Independence Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-09-02"",
        ""localName"": ""Labor Day"",
        ""name"": ""Labour Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-10-14"",
        ""localName"": ""Columbus Day"",
        ""name"": ""Columbus Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": false,
        ""counties"": [
            ""US-AL"",
            ""US-AZ"",
            ""US-CO"",
            ""US-CT"",
            ""US-GA"",
            ""US-ID"",
            ""US-IL"",
            ""US-IN"",
            ""US-IA"",
            ""US-KS"",
            ""US-KY"",
            ""US-LA"",
            ""US-ME"",
            ""US-MD"",
            ""US-MA"",
            ""US-MS"",
            ""US-MO"",
            ""US-MT"",
            ""US-NE"",
            ""US-NH"",
            ""US-NJ"",
            ""US-NM"",
            ""US-NY"",
            ""US-NC"",
            ""US-OH"",
            ""US-OK"",
            ""US-PA"",
            ""US-RI"",
            ""US-SC"",
            ""US-TN"",
            ""US-UT"",
            ""US-VA"",
            ""US-WV""
        ],
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-11-11"",
        ""localName"": ""Veterans Day"",
        ""name"": ""Veterans Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-11-28"",
        ""localName"": ""Thanksgiving Day"",
        ""name"": ""Thanksgiving Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": 1863,
        ""type"": ""Public""
    },
    {
        ""date"": ""2024-12-25"",
        ""localName"": ""Christmas Day"",
        ""name"": ""Christmas Day"",
        ""countryCode"": ""US"",
        ""fixed"": false,
        ""global"": true,
        ""counties"": null,
        ""launchYear"": null,
        ""type"": ""Public""
    }
        ]";
        return JsonConvert.DeserializeObject<List<PublicHoliday>>(json);
    }

    static bool IsPublicHoliday(DateTime date, List<PublicHoliday> publicHolidays)
    {
        foreach (var holiday in publicHolidays)
        {
            if (holiday.Date.Date == date.Date)
                return true;
        }
        return false;
    }
}
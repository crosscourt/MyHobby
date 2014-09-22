using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace MyHobby.Migrations
{
    public class DataImporter
    {
        public void ImportSuburbs(MyHobbyContext context)
        {
            List<Suburb> suburbs = new List<Suburb>();

            StreamReader sr = null;
            try
            {
                Assembly resourceAssembly = Assembly.GetAssembly(typeof(MyHobby.Resources.LocateThisAssembly));
                Stream stream = resourceAssembly.GetManifestResourceStream("MyHobby.Resources.Suburbs_HK.txt");

                sr = new StreamReader(stream);
                if (sr != null)
                {
                    SuburbGroup group = null;
                    string line = sr.ReadLine();

                    while (!string.IsNullOrEmpty(line))
                    {
                        string[] fields = SplitCSV(line); //line.Split(',');
                                                
                        if (fields.Length == 3)
                        {
                            group = new SuburbGroup()
                            {
                                Name = fields[1],
                                EnglishName = fields[2]
                            };

                            context.SuburbGroups.Add(group);                            
                        }
                        else
                        {
                            Suburb suburb = new Suburb()
                            {
                                Name = fields[0],
                                EnglishName = fields[1],
                                SuburbGroup = group
                            };

                            context.Suburbs.Add(suburb);   
                        }
                        
                        line = sr.ReadLine();
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        private string[] SplitCSV(string input)
        {
            string pattern = ",(?!(?:[^\",]|[^\"],[^\"])+\")";
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }
    }
}
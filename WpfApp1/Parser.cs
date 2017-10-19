using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphicalSubtitles
{
    class Parser
    {
        private List<Subtitles> Subtitles = new List<Subtitles>();
        private TextBox DisplaySub;

        public Parser(TextBox Sub)
        {
            DisplaySub = Sub;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileRecovery(path + @"\MrRobot-trailer.txt");
            DisplaySubtitles();
        }

        public void FileRecovery(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    //Initialisation variables
                    string line;
                    string ST = null;
                    TimeSpan start = TimeSpan.Parse("00:00");
                    TimeSpan stop = TimeSpan.Parse("00:00");
                    string[] split = null;



                    while ((line = reader.ReadLine()) != null)
                    {
                        //Regex pour récupérer les temps et les sous-titres
                        Regex time = new Regex(@"^\d\d:\d\d:\d\d,\d\d\d");
                        Regex text = new Regex(@"^(.).+(\r\n|$)");
                        Regex index = new Regex(@"^\d{1,4}$");

                        //Récupération des temps et des sous-titres tant que les lignes correspondent aux Regex
                        if ((time.Match(line).Success || text.Match(line).Success) && !index.Match(line).Success)
                        {
                            //Récupération de la ligne des temps
                            if (time.Match(line).Success)
                            {
                                //séparation de la ligne en 3 : "00:00" "-->" "00:00"
                                split = line.Split(' ');
                                start = TimeSpan.Parse(split[0]);
                                stop = TimeSpan.Parse(split[2]);
                            }

                            //Récupération des sous-titres
                            else if (text.Match(line).Success)
                            {
                                if (ST == null)
                                {
                                    ST = line;
                                }
                                //Si la variable ST contient déjà un sous-titre, on lui rajoute le sous-titre suivant
                                else
                                {
                                    ST += "\n" + line;
                                }
                            }
                        }

                        //Lorsque les lignes ne correspondent plus aux regex (numéro de sous-titre ou ligne vide)
                        else
                        {
                            //On vérifie que les valeurs sont bien stockées dans les variables
                            if (ST != null && start != TimeSpan.Parse("00:00") && stop != TimeSpan.Parse("00:00"))
                            {
                                //On ajoute un nouvel objet Subtitles à la liste des sous-titres
                                Subtitles.Add(new Subtitles(start, stop, ST));

                                //On réinitialise les variables pour stocker les données des sous-titres suivants
                                ST = null;
                                start = TimeSpan.Parse("00:00");
                                stop = TimeSpan.Parse("00:00");

                            }
                        }
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                DisplaySub.Text = (e.Message);
                Console.WriteLine(e.Message);
            }


        }

        public async Task DisplaySubtitles()
        {
            //Affichage premier sous-titre
            await Task.Delay(Subtitles[0].start);
            DisplaySub.Text = Subtitles[0].ST;
 
            await Task.Delay(Subtitles[0].stop - Subtitles[0].start);
            DisplaySub.Clear();
            for (int i = 1; i < Subtitles.Count; i++)
            {
                await Task.Delay(Subtitles[i].start - Subtitles[i - 1].stop);
                DisplaySub.Text = Subtitles[i].ST;

                await Task.Delay(Subtitles[i].stop - Subtitles[i].start);
                DisplaySub.Clear();
            }
        }
    }
}

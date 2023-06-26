using Newtonsoft.Json;

namespace Questao2;

class Program
{
    static async Task Main()
    {
        await GetTotalScoredGoals("Paris Saint-Germain", 2013);
        await GetTotalScoredGoals("Chelsea", 2014);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    static async Task GetTotalScoredGoals(string teamName, int year)
    {
        string apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}";

        using (HttpClient client = new HttpClient())
        {
            int totalGoals = 0;
            int currentPage = 1;
            int totalPages = 1;

            while (currentPage <= totalPages)
            {
                string pageUrl = $"{apiUrl}&page={currentPage}";
                HttpResponseMessage response = await client.GetAsync(pageUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    FootballMatchesData matchesData = JsonConvert.DeserializeObject<FootballMatchesData>(json);

                    foreach (FootballMatch match in matchesData.Data)
                    {
                        if (match.Team1 == teamName)
                        {
                            totalGoals += Convert.ToInt32(match.Team1Goals);
                        }

                        if (match.Team2 == teamName)
                        {
                            totalGoals += Convert.ToInt32(match.Team2Goals);
                        }
                    }

                    totalPages = matchesData.Total_Pages;
                    currentPage++;
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                    break;
                }
            }

            Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");
        }
    }
}
using BullkyBook.Models;
using DataBase;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BullkyBook.Services
{
    public class BookCategoryService 
    {
        private readonly ApplicationDbContext _db;
        private readonly string Url = "https://openlibrary.org/subjects/arts.json";

        public BookCategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> FeatchAndImport()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    
                    HttpResponseMessage response = await client.GetAsync(Url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    // Parse JSON response
                    var jsonDoc = JsonDocument.Parse(responseBody);
                    var root = jsonDoc.RootElement;
                    
                    // Extract works array from Open Library API
                    if (root.TryGetProperty("works", out var works))
                    {
                        var categoriesAdded = 0;
                        var displayOrder = 1;
                        
                        foreach (var work in works.EnumerateArray())
                        {
                            // Extract subject from each work
                            if (work.TryGetProperty("subject", out var subjects))
                            {
                                foreach (var subject in subjects.EnumerateArray())
                                {
                                    string? subjectName = subject.GetString();
                                    
                                    if (!string.IsNullOrEmpty(subjectName))
                                    {
                                        // Check if category already exists
                                        var existingCategory = _db.Catagories
                                            .FirstOrDefault(c => c.Name == subjectName);
                                        
                                        if (existingCategory == null)
                                        {
                                            var category = new Category
                                            {
                                                Name = subjectName,
                                                DisbplayOrder = displayOrder++,
                                                CreatedDateTime = DateTime.Now
                                            };
                                            
                                            _db.Catagories.Add(category);
                                            categoriesAdded++;
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (categoriesAdded > 0)
                        {
                            await _db.SaveChangesAsync();
                            return true;
                        }
                    }
                    
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
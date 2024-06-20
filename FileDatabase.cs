namespace FileDb
{
    public class FileDatabase
    {
        private readonly string databasePath;
        private readonly Dictionary<string, string> keyValueStore;

        public FileDatabase(string path)
        {
            databasePath = path;
            keyValueStore = [];

            if (File.Exists(databasePath))
            {
                LoadFromFile();
            }
            else
            {
                SaveToFile();
            }
        }

        // 데이터 파일에서 데이터 로드
        private void LoadFromFile()
        {
            try
            {
                var lines = File.ReadAllLines(databasePath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            keyValueStore[parts[0]] = parts[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading database: {ex.Message}");
            }
        }

        private void SaveToFile()
        {
            try
            {
                using StreamWriter writer = new(databasePath);
                foreach (var entry in keyValueStore)
                {
                    writer.WriteLine($"{entry.Key}|{entry.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving database: {ex.Message}");
            }
        }

        public string? Get(string key)
        {
            return keyValueStore.TryGetValue(key, out string? value) ? value : null;
        }

        public void Set(string key, string value)
        {
            keyValueStore[key] = value;
            SaveToFile();
        }

        public void Delete(string key)
        {
            if (keyValueStore.Remove(key))
            {
                SaveToFile();
            }
        }

        public void PrintAll()
        {
            foreach (var entry in keyValueStore)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }

            Console.WriteLine();
        }

        public List<string> Query(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return keyValueStore.Where(predicate)
                                .Select(kvp => $"{kvp.Key}: {kvp.Value}")
                                .ToList();
        }
    }
}
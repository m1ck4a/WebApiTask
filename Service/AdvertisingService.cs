namespace WebApplicationTask.Service
{
    public class AdvertisingService
    {
        private List<AdvertisingPlatformModel> _platforms = new List<AdvertisingPlatformModel>();

        public void LoadDataFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.");
            }

            _platforms.Clear();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':', 2);
                if (parts.Length != 2)
                    continue;

                var platformName = parts[0].Trim();
                var locations = parts[1].Split(',').Select(l => l.Trim()).ToList();

                _platforms.Add(new AdvertisingPlatformModel
                {
                    Name = platformName,
                    Locations = locations
                });
            }
        }

        public List<string> GetPlatformsForLocation(string location)
        {
            var result = new List<string>();

            foreach (var platform in _platforms)
            {
                if (platform.Locations.Any(l => location.StartsWith(l)))
                {
                    result.Add(platform.Name);
                }
            }

            return result.OrderBy(p => _platforms.First(x => x.Name == p).Locations.Min(l => l.Count(c => c == '/'))).ToList();
        }
    }
}

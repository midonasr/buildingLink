namespace buildingLink.Repositories
{
    using buildingLink.Models;
    using System.Data; 
    using System.Data.SQLite;
    using Dapper;
    public class DriverRepository : IDriverRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public DriverRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("default");
        }
        public async Task<Driver> AddDriverAsync(Driver person)
        {
            using IDbConnection connection = new SQLiteConnection(_connectionString);
            string sql = @"insert into Driver (FirstName,LastName,Email,PhoneNumber) values (@FirstName,@LastName,@Email,@PhoneNumber);
                       SELECT last_insert_rowid()";
            int createdId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                person.FirstName,
                person.LastName,
                person.Email,
                person.PhoneNumber
            });
            person.Id = createdId;
            return person;
        }

        public async Task DeleteDriverAsync(int id)
        {
            using IDbConnection connection = new SQLiteConnection(_connectionString);
            string sql = @"delete from Driver where Id=@id";
            await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<IEnumerable<Driver>> GetDriversAsync()
        {
            using IDbConnection connection = new SQLiteConnection(_connectionString);
            string sql = "select * from Driver";
            var drivers=  await connection.QueryAsync<Driver>(sql);
            foreach (var item in drivers.ToList())
            {
                item.FirstName = string.Join("", item.FirstName.ToLower().ToCharArray().OrderBy(a => a).ToList());
                item.LastName = string.Join("", item.LastName.ToLower().ToCharArray().OrderBy(a => a).ToList());
            }
            return drivers;
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            using IDbConnection connection = new SQLiteConnection(_connectionString);
            string sql = "select * from Driver where Id=@id";
            return await connection.QueryFirstOrDefaultAsync<Driver>(sql, new { id });
            
        }

        public async Task UpdateDriverAsync(Driver person)
        {
            using IDbConnection connection = new SQLiteConnection(_connectionString);
            string sql = @"update Driver set FirstName=@FirstName,LastName=@LastName,PhoneNumber=@PhoneNumber,Email=@Email where Id=@Id";
            await connection.ExecuteAsync(sql, new {  person.FirstName, person.LastName, Email = person.Email, person.PhoneNumber, Id = person.Id });
        }
    }
}

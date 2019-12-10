using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class PeopleRepository : IRepository<People>
    {
        private readonly string SQL_SELECT_ALL = "SELECT id, \"user\", name, surname, email, language, admin, password FROM public.people order by people.id;";
        private readonly string SQL_SELECT = "SELECT id, \"user\", name, surname, email, language, admin, password FROM public.people where people.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.people SET \"user\"=@1, name=@2, surname=@3, email=@4, language=@5, admin=@6, password=@7 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.people WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.people(\"user\", name, surname, email, language, admin, password) VALUES (@1, @2, @3, @4, @5, @6, @7);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.people(id, \"user\", name, surname, email, language, admin, password) VALUES (@0, @1, @2, @3, @4, @5, @6, @7);";

        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<People> AddAsync(People entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.user);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.surname);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.email);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.language);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Boolean, entity.admin);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Text, entity.password);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new People
                        {
                            user = reader.GetString(1),
                            name = reader.GetString(2),
                            surname = reader.GetString(3),
                            email = reader.GetString(4),
                            language = reader.GetString(5),
                            admin = reader.GetBoolean(6),
                            password = reader.GetString(7),
                        };
                    }
                }
            }
        }

        public async Task<People> EditAsync(People entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.user);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.surname);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.email);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.language);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Boolean, entity.admin);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Text, entity.password);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new People
                        {
                            Id = reader.GetInt64(0),
                            user = reader.GetString(1),
                            name = reader.GetString(2),
                            surname = reader.GetString(3),
                            email = reader.GetString(4),
                            language = reader.GetString(5),
                            admin = reader.GetBoolean(6),
                            password = reader.GetString(7),
                        };
                    }
                }
            }
        }

        public async Task<People[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<People>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new People
                            {
                                Id = reader.GetInt64(0),
                                user = reader.GetString(1),
                                name = reader.GetString(2),
                                surname = reader.GetString(3),
                                email = reader.GetString(4),
                                language = reader.GetString(5),
                                admin = reader.GetBoolean(6),
                                password = reader.GetString(7),
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<People> GetAsync(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_SELECT;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new People
                        {
                            Id = reader.GetInt64(0),
                            user = reader.GetString(1),
                            name = reader.GetString(2),
                            surname = reader.GetString(3),
                            email = reader.GetString(4),
                            language = reader.GetString(5),
                            admin = reader.GetBoolean(6),
                            password = reader.GetString(7),
                        };
                    }
                }
            }
        }

        public async Task<People> RemoveAsync(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_DELETE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new People
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public async Task<People> AddId(People entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.user);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.surname);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.email);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.language);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Boolean, entity.admin);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Text, entity.password);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new People
                        {
                            Id = reader.GetInt64(0),
                            user = reader.GetString(1),
                            name = reader.GetString(2),
                            surname = reader.GetString(3),
                            email = reader.GetString(4),
                            language = reader.GetString(5),
                            admin = reader.GetBoolean(6),
                            password = reader.GetString(7),
                        };
                    }
                }
            }
        }
    }
}

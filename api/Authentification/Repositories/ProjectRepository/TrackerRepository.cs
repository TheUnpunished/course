using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class TrackerRepository : IRepository<Tracker>
    {
        private readonly string SQL_SELECT_ALL = "SELECT id, name, status FROM public.tracker order by tracker.id;";
        private readonly string SQL_SELECT = "SELECT id, name, status FROM public.tracker where tracker.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.tracker SET name=@1, status=@2 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.tracker WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.tracker(name, status) VALUES (@1, @2);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.tracker(id, name, status) VALUES (@0, @1, @2);";

        private readonly string _connectionString;

        public TrackerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Tracker> AddAsync(Tracker entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.status);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tracker
                        {
                            name = reader.GetString(1),
                            status = reader.GetString(2),
                        };
                    }
                }
            }
        }

        public async Task<Tracker> AddId(Tracker entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.status);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tracker
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            status = reader.GetString(2),
                        };
                    }
                }
            }
        }

        public async Task<Tracker> EditAsync(Tracker entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.status);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tracker
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            status = reader.GetString(2),
                        };
                    }
                }
            }
        }

        public async Task<Tracker[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Tracker>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Tracker
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                status = reader.GetString(2)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Tracker> GetAsync(long id)
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
                        return new Tracker
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            status = reader.GetString(2)
                        };
                    }
                }
            }
        }

        public async Task<Tracker> RemoveAsync(long id)
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
                        return new Tracker
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }
    }
}

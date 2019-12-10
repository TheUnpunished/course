using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class SLogRepository : IRepository<SLog>
    {
        private readonly string SQL_SELECT_ALL = "SELECT slog.id, slog.\"time\", task.id FROM slog, public.\"task\" where slog.task_id = public.\"task\".id order by slog.id;";
        private readonly string SQL_SELECT = "SELECT slog.id, slog.\"time\", task.id FROM slog, public.\"task\" where slog.task_id = public.\"task\".id and slog.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE slog SET \"time\"=@1, task_id=@2 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM slog WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO slog(\"time\", task_id) VALUES (@1, @2);";
        private readonly string SQL_INSERT_ID = "INSERT INTO slog(id, \"time\", task_id) VALUES (@0, @1, @2);";

        private readonly string _connectionString;

        public SLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SLog> AddAsync(SLog entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.TimestampTz, entity.time);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new SLog
                        {
                            time = reader.GetDateTime(1),
                            taskid = reader.GetInt64(2)
                        };
                    }
                }
            }
        }

        public async Task<SLog> AddId(SLog entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.TimestampTz, entity.time);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new SLog
                        {
                            Id = reader.GetInt64(0),
                            time = reader.GetDateTime(1),
                            taskid = reader.GetInt64(2)
                        };
                    }
                }
            }
        }

        public async Task<SLog> EditAsync(SLog entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.TimestampTz, entity.time);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new SLog
                        {
                            Id = reader.GetInt64(0),
                            time = reader.GetDateTime(1),
                            taskid = reader.GetInt64(2),
                        };
                    }
                }
            }
        }

        public async Task<SLog[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<SLog>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new SLog
                            {
                                Id = reader.GetInt64(0),
                                time = reader.GetDateTime(1),
                                taskid = reader.GetInt64(2),
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<SLog> GetAsync(long id)
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
                        return new SLog
                        {
                            Id = reader.GetInt64(0),
                            time = reader.GetDateTime(1),
                            taskid = reader.GetInt64(2),
                        };
                    }
                }
            }
        }

        public async Task<SLog> RemoveAsync(long id)
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
                        return new SLog
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }
    }
}

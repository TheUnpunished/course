using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class LogRepository : IRepository<Log>
    {
        private readonly string SQL_SELECT_ALL = "SELECT log.id, log.name, task.id, task.status, task.percent, log.description, \"file_Path\" FROM public.log join task on log.task_id = task.id order by log.id;";
        private readonly string SQL_SELECT = "SELECT log.id, log.name, task.id, task.status, task.percent, log.description, \"file_Path\" FROM public.log join task on log.task_id = task.id where log.id =@0;";
        private readonly string SQL_UPDATE = "UPDATE public.log SET name=@1, task_id=@2, description=@3, \"file_Path\"=@4 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.log WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.log(name, task_id, description, \"file_Path\") VALUES (@1, @2, @3, @4);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.log(id, name, task_id, description, \"file_Path\") VALUES (@0, @1, @2, @3, @4);";

        private readonly string _connectionString;

        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Log> AddAsync(Log entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.file_path);

                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Log
                        {
                            name = reader.GetString(1),
                            taskid = reader.GetInt64(2),
                            description = reader.GetString(3),
                            file_path = reader.GetString(4)
                        };
                    }
                }
            }
        }

        public async Task<Log> AddId(Log entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.file_path);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Log
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            taskid = reader.GetInt64(2),
                            description = reader.GetString(3),
                            file_path = reader.GetString(4)
                        };
                    }
                }
            }
        }

        public async Task<Log> EditAsync(Log entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.taskid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.file_path);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Log
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            taskid = reader.GetInt64(2),
                            description = reader.GetString(3),
                            file_path = reader.GetString(4)
                        };
                    }
                }
            }
        }

        public async Task<Log[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Log>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Log
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                task_id = new Tasks
                                {
                                    Id = reader.GetInt64(2),
                                    status = reader.GetString(3),
                                    percent = reader.GetString(4),
                                },
                                description = reader.GetString(5),
                                file_path = reader.GetString(6)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Log> GetAsync(long id)
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
                        return new Log
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            task_id = new Tasks
                            {
                                Id = reader.GetInt64(2),
                                status = reader.GetString(3),
                                percent = reader.GetString(4),
                            },
                            description = reader.GetString(5),
                            file_path = reader.GetString(6)
                        };
                    }
                }
            }
        }

        public async Task<Log> RemoveAsync(long id)
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
                        return new Log
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }
    }
}

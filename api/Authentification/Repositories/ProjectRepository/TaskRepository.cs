using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class TasksRepository : IRepository<Tasks>
    {
        private readonly string SQL_SELECT_ALL = "SELECT task.id, task.theme, tracker.id, tracker.name, task.status, task.priority, task.description, category.id, category.name, spring.id, spring.name, task.date_over, task.time, task.percent, task.file_path,people.user,people.id, project.id FROM public.task join tracker on tracker.id = task.tracker join category on category.id = task.category join spring on spring.id = task.spring join people on people.id = task.whocan join project on project.id = task.project_id order by task.id;";
        private readonly string SQL_SELECT = "SELECT task.id, task.theme, tracker.id, tracker.name, task.status, task.priority, task.description, category.id, category.name, spring.id, spring.name, task.date_over, task.time, task.percent, task.file_path,people.user, people.id, project.id FROM public.task join tracker on tracker.id = task.tracker join category on category.id = task.category join spring on spring.id = task.spring join people on people.id = task.whocan join project on project.id = task.project_id where task.id=@0";
        private readonly string SQL_UPDATE = "UPDATE public.task SET theme=@1, tracker=@2, status=@3, priority=@4, description=@5, category=@6, spring=@7, date_over=@8, \"time\"=@9, percent=@10, file_path=@11, \"whocan\"=@12, project_id=@13 WHERE id=@0;";
        private readonly string SQL_DELETE = "DELETE FROM public.task WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.task(theme, tracker, status, priority, description, category, spring, date_over, \"time\", percent, file_path, \"whocan\", project_id) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.task(id, theme, tracker, status, priority, description, category, spring, date_over, \"time\", percent, file_path, \"whocan\", project_id) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13);";

        private readonly string _connectionString;

        public TasksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Tasks> AddAsync(Tasks entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.theme);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.trackerid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.priority);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Bigint, entity.categoryid);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Bigint, entity.springid);
                    comm.Parameters.AddWithValue("8", NpgsqlDbType.TimestampTz, entity.date_over);
                    comm.Parameters.AddWithValue("9", NpgsqlDbType.Integer, entity.time);
                    comm.Parameters.AddWithValue("10", NpgsqlDbType.Text, entity.percent);
                    comm.Parameters.AddWithValue("11", NpgsqlDbType.Text, entity.file_path);
                    comm.Parameters.AddWithValue("12", NpgsqlDbType.Bigint, entity.whocanid);
                    comm.Parameters.AddWithValue("13", NpgsqlDbType.Bigint, entity.projectid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tasks
                        {
                            theme = reader.GetString(1),
                            trackerid = reader.GetInt64(2),
                            status = reader.GetString(3),
                            priority = reader.GetString(4),
                            description = reader.GetString(5),
                            categoryid = reader.GetInt64(6),
                            springid = reader.GetInt64(7),
                            date_over = reader.GetDateTime(8),
                            time = reader.GetInt32(9),
                            percent = reader.GetString(10),
                            file_path = reader.GetString(11),
                            whocanid = reader.GetInt64(12),
                            projectid = reader.GetInt64(13),
                        };
                    }
                }
            }
        }

        public async Task<Tasks> EditAsync(Tasks entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.theme);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.trackerid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.priority);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Bigint, entity.categoryid);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Bigint, entity.springid);
                    comm.Parameters.AddWithValue("8", NpgsqlDbType.TimestampTz, entity.date_over);
                    comm.Parameters.AddWithValue("9", NpgsqlDbType.Integer, entity.time);
                    comm.Parameters.AddWithValue("10", NpgsqlDbType.Text, entity.percent);
                    comm.Parameters.AddWithValue("11", NpgsqlDbType.Text, entity.file_path);
                    comm.Parameters.AddWithValue("12", NpgsqlDbType.Bigint, entity.whocanid);
                    comm.Parameters.AddWithValue("13", NpgsqlDbType.Bigint, entity.projectid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tasks
                        {
                            Id = reader.GetInt64(0),
                            theme = reader.GetString(1),
                            trackerid = reader.GetInt64(2),
                            status = reader.GetString(3),
                            priority = reader.GetString(4),
                            description = reader.GetString(5),
                            categoryid = reader.GetInt64(6),
                            springid = reader.GetInt64(7),
                            date_over = reader.GetDateTime(8),
                            time = reader.GetInt32(9),
                            percent = reader.GetString(10),
                            file_path = reader.GetString(11),
                            whocanid = reader.GetInt64(12),
                            projectid = reader.GetInt64(13),
                        };
                    }
                }
            }
        }

        public async Task<Tasks[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Tasks>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Tasks
                            {
                                Id = reader.GetInt64(0),
                                theme = reader.GetString(1),
                                tracker = new Tracker {
                                    Id = reader.GetInt64(2),
                                    name = reader.GetString(3)
                                },
                                status = reader.GetString(4),
                                priority = reader.GetString(5),
                                description = reader.GetString(6),
                                category = new Category {
                                    Id = reader.GetInt64(7),
                                    name = reader.GetString(8)
                                },
                                spring = new Spring {
                                    Id = reader.GetInt64(9),
                                    name = reader.GetString(10)
                                },
                                date_over = reader.GetDateTime(11),
                                time = reader.GetInt32(12),
                                percent = reader.GetString(13),
                                file_path = reader.GetString(14),
                                whoCan = new People {
                                    user = reader.GetString(15),
                                    Id = reader.GetInt64(16)
                                },
                                project_id = new Project
                                {
                                    Id = reader.GetInt64(17),
                                }

                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Tasks> GetAsync(long id)
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
                        return new Tasks
                        {
                            Id = reader.GetInt64(0),
                            theme = reader.GetString(1),
                            tracker = new Tracker
                            {
                                Id = reader.GetInt64(2),
                                name = reader.GetString(3)
                            },
                            status = reader.GetString(4),
                            priority = reader.GetString(5),
                            description = reader.GetString(6),
                            category = new Category
                            {
                                Id = reader.GetInt64(7),
                                name = reader.GetString(8)
                            },
                            spring = new Spring
                            {
                                Id = reader.GetInt64(9),
                                name = reader.GetString(10)
                            },
                            date_over = reader.GetDateTime(11),
                            time = reader.GetInt32(12),
                            percent = reader.GetString(13),
                            file_path = reader.GetString(14),
                            whoCan = new People
                            {
                                user = reader.GetString(15),
                                Id = reader.GetInt64(16)
                            },
                            project_id = new Project
                            {
                                Id = reader.GetInt64(17)
                            }
                        };
                    }
                }
            }
        }

        public async Task<Tasks> RemoveAsync(long id)
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
                        return new Tasks
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public async Task<Tasks> AddId(Tasks entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.theme);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.trackerid);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Text, entity.priority);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("6", NpgsqlDbType.Bigint, entity.categoryid);
                    comm.Parameters.AddWithValue("7", NpgsqlDbType.Bigint, entity.springid);
                    comm.Parameters.AddWithValue("8", NpgsqlDbType.TimestampTz, entity.date_over);
                    comm.Parameters.AddWithValue("9", NpgsqlDbType.Integer, entity.time);
                    comm.Parameters.AddWithValue("10", NpgsqlDbType.Text, entity.percent);
                    comm.Parameters.AddWithValue("11", NpgsqlDbType.Text, entity.file_path);
                    comm.Parameters.AddWithValue("12", NpgsqlDbType.Bigint, entity.whocanid);
                    comm.Parameters.AddWithValue("13", NpgsqlDbType.Bigint, entity.projectid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Tasks
                        {
                            Id = reader.GetInt64(0),
                            theme = reader.GetString(1),
                            trackerid = reader.GetInt64(2),
                            status = reader.GetString(3),
                            priority = reader.GetString(4),
                            description = reader.GetString(5),
                            categoryid = reader.GetInt64(6),
                            springid = reader.GetInt64(7),
                            date_over = reader.GetDateTime(8),
                            time = reader.GetInt32(9),
                            percent = reader.GetString(10),
                            file_path = reader.GetString(11),
                            whocanid = reader.GetInt64(12),
                            projectid = reader.GetInt64(13),
                        };
                    }
                }
            }
        }
    }
}

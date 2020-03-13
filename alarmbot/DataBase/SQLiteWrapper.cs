// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alarmbot.DataBase
{
    public abstract class SQLiteColumnModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }

    public class SQLiteWrapper<T> where T : SQLiteColumnModel, new()
    {
        object dblock = new object();
        string dbpath;

        public SQLiteWrapper(string dbpath)
        {
            this.dbpath = dbpath;

            var db = new SQLiteConnection(dbpath);
            var info = db.GetTableInfo(typeof(T).Name);
            if (!info.Any())
                db.CreateTable<T>();
            db.Close();
        }

        public void Add(T dbm)
        {
            lock (dblock)
            {
                var db = new SQLiteConnection(dbpath);
                db.Insert(dbm);
                db.Close();
            }
        }

        public void Update(T dbm)
        {
            lock (dblock)
            {
                var db = new SQLiteConnection(dbpath);
                db.Update(dbm);
                db.Close();
            }
        }

        public List<T> QueryAll()
        {
            lock (dblock)
            {
                using (var db = new SQLiteConnection(dbpath))
                    return db.Table<T>().ToList();
            }
        }

        public List<T> Query(string where)
        {
            lock (dblock)
            {
                var db = new SQLiteConnection(dbpath);
                return db.Query<T>($"select * from {typeof(T).Name} where {where}");
            }
        }

        public List<T> Query(Func<T, bool> func)
        {
            lock (dblock)
            {
                var db = new SQLiteConnection(dbpath);
                return db.Table<T>().Where(func).ToList();
            }
        }
    }
}

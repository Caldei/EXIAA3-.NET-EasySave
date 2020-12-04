﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel;

namespace EasySave.NS_Model
{
    class Work : INotifyPropertyChanged
    {
        // --- Attributes ---
        public event PropertyChangedEventHandler PropertyChanged;

        // Prepare options to indent JSON Files
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        private string nameP;
        public string name
        {
            get
            {
                return nameP;
            }
            set
            {
                if (value != nameP)
                {
                    nameP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("name"));
                    }
                }
            }
        }

        private string srcP;
        public string src
        {
            get
            {
                return srcP;
            }
            set
            {
                if (value != srcP)
                {
                    srcP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("src"));
                    }
                }
            }
        }

        private string dstP;
        public string dst
        {
            get
            {
                return dstP;
            }
            set
            {
                if (value != dstP)
                {
                    dstP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("dst"));
                    }
                }
            }
        }

        private BackupType backupTypeP;
        public BackupType backupType
        {
            get
            {
                return backupTypeP;
            }
            set
            {
                if (value != backupTypeP)
                {
                    backupTypeP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("backupType"));
                    }
                }
            }
        }

        private bool isCryptedP;
        public bool isCrypted
        {
            get
            {
                return isCryptedP;
            }
            set
            {
                if (value != isCryptedP)
                {
                    isCryptedP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("backupType"));
                    }
                }
            }
        }

        private State stateP;
        public State state {
            get
            {
                return stateP;
            }
            set
            {
                if (value != stateP)
                {
                    stateP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("state"));
                    }
                }
            }
        }

        private string lastBackupDateP;
        public string lastBackupDate
        {
            get
            {
                return lastBackupDateP;
            }
            set
            {
                if (value != lastBackupDateP)
                {
                    lastBackupDateP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("lastBackupDate"));
                    }
                }
            }
        }


        // --- Constructors ---
        // Constructor used by LoadWorks()
        public Work() { }

        // Constructor used by AddWork()
        public Work(string _name, string _src, string _dst, BackupType _backupType, bool _isCrypted)
        {
            this.name = _name;
            this.src = _src;
            this.dst = _dst;
            this.backupType = _backupType;
            this.isCrypted = _isCrypted;
            this.state = null;
        }


        // --- Methods ----
        // Save Log 
        public void SaveLog(DateTime _startDate, string _src, string _dst, long _size, bool isError)
        {
            // Prepare dates
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string startTime = _startDate.ToString("yyyy-MM-dd_HH-mm-ss");
            string elapsedTime = (DateTime.Now - _startDate).ToString();

            if (isError)
            {
                elapsedTime = "-1";
            }

            // Create File if it doesn't exists
            if (!Directory.Exists("./Logs"))
            {
                Directory.CreateDirectory("./Logs");
            }

            // Var that will contains Logs File Content
            var logs = new List<Log>();

            // Get Logs File Content if it Exists
            if (File.Exists($"./Logs/{today}.json"))
            {
                logs = JsonSerializer.Deserialize<List<Log>>(File.ReadAllText($"./Logs/{today}.json"));
            }

            // Add Current Backuped File Log
            logs.Add(new Log($"{this.name}", $"{_src}", $"{_dst}", $"{_size}", $"{startTime}", $"{elapsedTime}"));

            // Write Logs File
            File.WriteAllText($"./Logs/{today}.json", JsonSerializer.Serialize(logs, this.jsonOptions));

            /*
            // Log in .txt version
            File.AppendAllText($"./Logs/{today}.txt", $"{startTime}: {this.name}" +
                $"\nSource: {_src}" +
                $"\nDestination: {_dst}" +
                $"\nSize (Bytes): {_size}" +
                $"\nElapsed Time: {elapsedTime}" +
                "\n\r\n");
            */
        }
    }
}

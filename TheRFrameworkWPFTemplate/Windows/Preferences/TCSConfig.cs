﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using TheRFramework.Utilities.String;
using $safeprojectname$.Windows.Logger;

namespace $safeprojectname$.Windows.Preferences
{
    // stands for "TheR Config Structure"... lol. technically just a YAML file with less supported features

    /// <summary>
    ///     A class for loading/saving a TCS config file (which i made up lol). 
    ///     This isn't a serialisable/deserialisable config, but more of a 
    ///     config similar to minecraft's YAML configs, normally used with plugins. Example: 
    /// <code>
    ///     config.GetString("My Field") // could return "My value here :)"
    /// </code>
    /// <para>
    ///     Mainly made this for fun and because WPF's builtin settings just seems a bit of a hassle
    /// </para>
    /// <para>
    ///     You can edit this config to your likings btw, but by default the config file is created in MyDocuments
    /// </para>
    /// made by TheR/Kettlesimulator/ThatThingCalledTheGalaxy/AngryCarrot789/Carrot... all my different names lol
    /// </summary>
    public class TCSConfig
    {
        public const string DEFAULT_CONFIG_FILE_NAME = "config";
        public const string DEFAULT_CONFIG_DIRECTORY_NAME = "$safeprojectname$";

        public Dictionary<string, string> StringValues { get; set; }
        public Dictionary<string, List<string>> ListValues { get; set; }

        public string ConfigPath { get; set; }
        public bool IsConfigLoaded { get; private set; }

        /// <summary>
        /// The main application config file
        /// </summary>
        public static TCSConfig Main { get; set; }

        #region Constructors

        /// <summary>
        /// Create an empty instance of the <see cref="TCSConfig"/>, with nothing loaded
        /// </summary>
        public TCSConfig()
        {
            IsConfigLoaded = false;
            StringValues = new Dictionary<string, string>();
            ListValues = new Dictionary<string, List<string>>();
            ConfigPath = "";
        }

        /// <summary>
        /// Create an instance of the <see cref="TCSConfig"/> and load a config from the given directory and config name
        /// <para>
        ///     If the config directory doesn't exist, you'll be asked if you want to create it. Same with the config file.
        ///     If you click no on both message boxes, you'll have no config file, and nothing will be loaded.
        /// </para>
        /// </summary>
        /// <param name="configDirectory"></param>
        /// <param name="configNameWithoutExtension">The config name without an extension (e.g. 'config', not 'config.yml'</param>
        public TCSConfig(string configDirectory, string configNameWithoutExtension)
        {
            IsConfigLoaded = false;
            StringValues = new Dictionary<string, string>();
            ListValues = new Dictionary<string, List<string>>();
            ConfigPath = "";

            if (!Directory.Exists(configDirectory))
            {
                ApplicationLogger.Log("Config", $"'{configDirectory} doesn't exist. Creating...");
                Directory.CreateDirectory(configDirectory);
            }

            string fullPath = Path.Combine(configDirectory, (configNameWithoutExtension + ".yml"));
            if (File.Exists(fullPath))
            {
                if (Load(fullPath))
                {
                    return;
                }
                else
                {
                    ApplicationLogger.Log("Config", $"Failed to load config from path: {fullPath}");
                }
            }
            else
            {
                ApplicationLogger.Log("Config", $"'{fullPath} doesn't exist. Creating...");
                File.WriteAllText(fullPath, " ");
                ConfigPath = fullPath;
            }
        }

        /// <summary>
        /// Create an instance of the <see cref="TCSConfig"/> and load a config from the default config directory in MyDocuments, and config name
        /// <para>
        ///     If the config directory doesn't exist, you'll be asked if you want to create it. Same with the config file.
        ///     If you click no on both message boxes, you'll have no config file, and nothing will be loaded.
        /// </para>
        /// </summary>
        /// <param name="configDirectory"></param>
        /// <param name="configNameWithoutExtension">The config name without an extension (e.g. 'config', not 'config.yml'</param>
        public TCSConfig(string configNameWithoutExtension)
        {
            IsConfigLoaded = false;
            StringValues = new Dictionary<string, string>();
            ListValues = new Dictionary<string, List<string>>();
            ConfigPath = "";

            string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DEFAULT_CONFIG_DIRECTORY_NAME);
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            string fullPath = Path.Combine(configDirectory, (configNameWithoutExtension + ".yml"));
            if (File.Exists(fullPath))
            {
                if (Load(fullPath))
                {
                    return;
                }
                else
                {
                    ApplicationLogger.Log("Config", $"Failed to load config from path: {fullPath}");
                }
            }
            else
            {
                File.WriteAllText(fullPath, " ");
                ConfigPath = fullPath;
            }
        }

        #endregion

        #region Loading and creating

        public bool Load(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                ApplicationLogger.Log("Config", "File does not exist");
                return false;
            }

            StringValues.Clear();
            ListValues.Clear();
            IsConfigLoaded = false;

            // YAML parser
            try
            {
                bool isNextLineList = false;
                string listName = "";
                List<string> currentList = new List<string>();
                ApplicationLogger.Log("Config", $"Loading config from '{ConfigPath}'");
                ApplicationLogger.Log("Config", "Paring YAML file...");
                string[] configLines = File.ReadAllLines(fullPath);
                for (int i = 0; i < configLines.Length; i++)
                {
                    string line = configLines[i];
                    bool containsComma = line.Contains(':');
                    if (line.IsEmpty() || line.TrimStart()[0] == '#')
                    {
                        // even if in the middle of a list, still allow empty spaces
                        continue;
                    }

                    // parse lists. cannot parse lists within a list... not yet atleast ;)
                    if (isNextLineList)
                    {
                        string trimmedListItemStart = line.TrimStart();
                        if (trimmedListItemStart[0] == '-')
                        {
                            string listItem = trimmedListItemStart.After("-").TrimStart().TrimEnd();
                            currentList.Add(listItem);

                            // last line
                            if ((i + 1) == configLines.Length)
                            {
                                if (isNextLineList)
                                {
                                    isNextLineList = false;
                                    ListValues.Add(listName, new List<string>(currentList));
                                    listName = "";
                                    currentList.Clear();
                                    break;
                                }
                            }

                            continue;
                        }
                        else
                        {
                            ListValues.Add(listName, new List<string>(currentList));
                            listName = "";
                            currentList.Clear();
                            isNextLineList = false;
                        }
                    }

                    // parse fields. "string" and "bool" pairs are treaded the same when loading and saving
                    // the app however parsed the "true" or "false" to a bool... see TryGetBoolean
                    string trimmedStart = line.TrimStart();
                    string trimmedEnd = line.TrimEnd();
                    string fieldName = trimmedStart.Before(":");
                    string fieldValue = trimmedEnd.After(":").TrimStart();
                    if (fieldName.IsEmpty() || fieldName[0] == ':')
                    {
                        continue;
                    }
                    // this is the only thing that can tell if looking for a field/value
                    // or a field/list. fieldValue will always be empty unless you put a
                    // value after the colon. if you put something after the colon then
                    // add the - listValues... idek what could happen but it could break
                    if (fieldValue.IsEmpty())
                    {
                        isNextLineList = true;
                        listName = fieldName;
                    }
                    else
                    {
                        StringValues.Add(fieldName, fieldValue);
                    }
                }

                ApplicationLogger.Log("Config", "Parsed successfully!");
                ConfigPath = fullPath;
                IsConfigLoaded = true;
                return true;
            }
            catch (Exception e)
            {
                ApplicationLogger.Log("Config", $"Failed to load config: {e.Message}");
                StringValues.Clear();
                ListValues.Clear();
                IsConfigLoaded = false;
                return false;
            }
        }

        public bool Reload()
        {
            return Load(ConfigPath);
        }

        public bool SaveConfig()
        {
            if (!File.Exists(ConfigPath))
            {
                ApplicationLogger.Log("Config", $"{ConfigPath} doesn't exist");
                return false;
            }

            // wont save the comments because it just wont... sry lol
            // initialising the "lines" with the number of strings + the number of lists and the 
            // assumed maximum amount of items every list has; 4
            List<string> configLines = new List<string>(StringValues.Count + (ListValues.Count * 4));
            foreach(KeyValuePair<string, string> pair in StringValues)
            {
                configLines.Add($"{pair.Key}: {pair.Value}");
            }

            foreach (KeyValuePair<string, List<string>> pair in ListValues)
            {
                configLines.Add($"{pair.Key}:");
                foreach(string field in pair.Value)
                {
                    configLines.Add($"  - {field}");
                }
            }

            string finalFile = string.Join("\n", configLines.ToArray());
            File.WriteAllText(ConfigPath, finalFile);

            return true;
        }

        #endregion

        #region Getting things

        /// <summary>
        /// Returns <see langword="true"/> if the field was successfully found, and it placed into <paramref name="fieldValue"/>.
        /// Otherwise it returns <see langword="false"/> and <paramref name="fieldValue"/> is <see langword="null"/>
        /// </summary>
        /// <param name="fieldName">bruh</param>
        /// <param name="fieldValue">bruh</param>
        /// <returns>Whether the field was successfuly found</returns>
        public bool TryGetString(string fieldName, out string fieldValue)
        {
            // could shrink this down 6 lines, but this is more readable imo :)
            if (StringValues.TryGetValue(fieldName, out string value))
            {
                fieldValue = value;
                return true;
            }
            else
            {
                fieldValue = null;
                return false;
            }
        }

        /// <summary>
        /// Very similar to <see cref="TryGetString(string, out string)"/> except it will try to parse its outpas as an enum value
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetEnum<TEnum>(string fieldName, out TEnum value) where TEnum : struct
        {
            value = default(TEnum);
            if (TryGetString(fieldName, out string stringValue))
            {
                if (Enum.TryParse(stringValue, true, out TEnum enumValue))
                {
                    value = enumValue;
                    return true;
                }
            }
            return false;
        }

        // these others do the exact same as above, but the list is sort of similar
        public bool TryGetInteger(string fieldName, out int fieldValue)
        {
            if (TryGetString(fieldName, out string value))
            {
                if (int.TryParse(value, out int intValue))
                {
                    fieldValue = intValue;
                    return true;
                }
                else
                {
                    fieldValue = 0;
                    return false;
                }
            }
            else
            {
                fieldValue = 0;
                return false;
            }
        }

        public bool TryGetBoolean(string fieldName, out bool fieldValue)
        {
            if (StringValues.TryGetValue(fieldName, out string value))
            {
                if (bool.TryParse(value, out bool boolValue))
                {
                    fieldValue = boolValue;
                    return true;
                }
                else
                {
                    fieldValue = false;
                    return false;
                }
            }
            else
            {
                fieldValue = false;
                return false;
            }
        }

        /// <summary>
        /// Returns a reference of items within the specific list
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="listValues"></param>
        /// <returns></returns>
        public bool TryGetList(string fieldName, out List<string> listValues)
        {
            if (ListValues.TryGetValue(fieldName, out List<string> values))
            {
                listValues = values;
                return true;
            }
            else
            {
                listValues = null;
                return false;
            }
        }

        /// <summary>
        /// Returns the same as <see cref="TryGetList(string, out List{string})"/> but outputs a copy of the list, no references.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="listValues"></param>
        /// <returns></returns>
        public bool TryGetListCopy(string fieldName, out List<string> listValues)
        {
            if (TryGetList(fieldName, out List<string> values))
            {
                listValues = new List<string>(values);
                return true;
            }
            else
            {
                listValues = null;
                return false;
            }
        }

        #endregion

        #region Setting things

        /// <summary>
        /// If the field exists in the config, it replaces it with <paramref name="fieldValue"/>. Otherwise, it is added
        /// to the config. If you dont want to add it, set the 3rd parameter to false
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        public void SetString(string fieldName, string fieldValue, bool addIfNotFound = true)
        {
            if (TryGetString(fieldName, out string unused))
            {
                StringValues[fieldName] = fieldValue;
            }
            else if (addIfNotFound)
            {
                StringValues.Add(fieldName, fieldValue);
            }
        }

        /// <summary>
        /// The exact same a <see cref="SetString(string, string, bool)"/> except it uses enums making it easier to use 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetEnum<TEnum>(string fieldName, TEnum enumValue, bool addIfNotFound = true) where TEnum : struct
        {
            if (TryGetString(fieldName, out string unused))
            {
                StringValues[fieldName] = enumValue.ToString();
            }
            else if (addIfNotFound)
            {
                StringValues.Add(fieldName, enumValue.ToString());
            }
        }

        public void SetInteger(string fieldName, int fieldValue, bool addIfNotFound = true)
        {
            SetString(fieldName, fieldValue.ToString(), addIfNotFound);
        }

        public void SetBoolean(string fieldName, bool fieldValue, bool addIfNotFound = true)
        {
            SetString(fieldName, fieldValue.ToString(), addIfNotFound);
        }

        /// <summary>
        /// Sets the <paramref name="fieldName"/> list's values list as 
        /// <paramref name="fieldValues"/>, meaning they're referenced
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValues"></param>
        /// <param name="addIfNotFound"></param>
        public void SetList(string fieldName, List<string> fieldValues, bool addIfNotFound = true)
        {
            if (ListValues.TryGetValue(fieldName, out List<string> unused))
            {
                ListValues[fieldName] = fieldValues;
            }
            else if (addIfNotFound)
            {
                ListValues.Add(fieldName, fieldValues);
            }
        }

        /// <summary>
        /// Does the same as <see cref="SetList(string, List{string}, bool)"/> except it sets the 
        /// list as a copy, meaning <paramref name="fieldValues"/> isn't referenced
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValues"></param>
        /// <param name="addIfNotFound"></param>
        public void SetListCopy(string fieldName, List<string> fieldValues, bool addIfNotFound = true)
        {
            SetList(fieldName, new List<string>(fieldValues), addIfNotFound);
        }

        #endregion
    }
}

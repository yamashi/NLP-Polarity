﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    [DataContract]
    public class SpeechEntity
    {
        [DataMember]
        private List<SpeechClass> speechClasses = new List<SpeechClass>();

        [DataMember]
        private string entity;

        [IgnoreDataMember]
        public SpeechEntity Left
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public SpeechEntity Right
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public int Width
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public string Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        public SpeechEntity(string entity)
        {
            this.entity = entity;
        }

        public SpeechEntity(SpeechEntity entity1, SpeechEntity entity2)
        {
            Left = entity1;
            Right = entity2;

            entity = "";// entity1.ToString() + " + " + entity2.ToString();
        }

        public SpeechEntity(SpeechEntity entity1, SpeechEntity entity2, double[] arr)
        {
            Left = entity1;
            Right = entity2;

            entity = "";

            // 0 is the bias
            for (int i = 1; i < 11; ++i)
            {
                if (arr[i] > 0.9)
                {
                    Add((SpeechClass)(i - 1));
                    entity += (SpeechClass)(i - 1) + " ";
                }
            }

            //entity = ".";// entity1.ToString() + " + " + entity2.ToString();
        }

        public void Add(SpeechClass c)
        {
            if (!speechClasses.Contains(c))
                speechClasses.Add(c);
        }

        public override string ToString() 
        {
            return entity;
        }

        public double[] ToArray()
        {
            double[] arr = new double[10];

            for (int i = 0; i < 10; ++i)
            {
                arr[i] = -1.0;
            }

            foreach (var e in speechClasses)
            {
                arr[(int)e] = 1.0;
            }

            return arr;
        }

        public static double[] Group(SpeechEntity firstEntity, SpeechEntity secondEntity)
        {
            double[] arr = new double[20];

            for (int i = 0; i < 20; ++i)
            {
                arr[i] = -1.0;
            }

            if(firstEntity.speechClasses != null)
                foreach (var e in firstEntity.speechClasses)
                {
                    arr[(int)e] = 1.0;
                }

            if (secondEntity.speechClasses != null)
                foreach (var e in secondEntity.speechClasses)
                {
                    arr[(int)e + 10] = 1.0;
                }

            return arr;
        }

        public static SpeechEntity Merge(SpeechEntity firstEntity, SpeechEntity secondEntity)
        {
            SpeechEntity ent = new SpeechEntity(firstEntity, secondEntity);

            foreach (var e in firstEntity.speechClasses)
            {
                ent.Add(e);
            }

            foreach (var e in secondEntity.speechClasses)
            {
                ent.Add(e);
            }

            return ent;
        }
    }
}

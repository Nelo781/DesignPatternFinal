using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Design_Pattern_Final_Project
{
    class MapReduce
    {
        //Result of the reduce step
        public ConcurrentDictionary<string, int> words_count;

        //Initialise/run the MapReduce and store the results in words_count
        public MapReduce(string text)
        {
            words_count = Reduce(Shuffle(Map(text)));
        }
        
        // Map the data (extract each word) with parallelized method 
        static ConcurrentBag<KeyValuePair<string, int>> Map(string value)
        {
            //Multithreading resistant list of objects
            ConcurrentBag<KeyValuePair<string, int>> result = new ConcurrentBag<KeyValuePair<string, int>>();
            
            //Parralelising the spliting process
            // We split the text by ignoring all the characters not interesting that pollute the data
            Parallel.ForEach(value.Split(' ',',','\n','.',';'), (word) =>
             {
                 
                 KeyValuePair<string, int> to_add = new KeyValuePair<string, int>(word.ToLower(), 1); // word => ToLower to avoid keys multiplicity
                 result.Add(to_add);
             });

            return result;
        }

        //Gathering the words together
        static ConcurrentDictionary<string, IList<int>> Shuffle(ConcurrentBag<KeyValuePair<string, int>> WordValPair)
        {
            // Store all the known words in a Multithreading resistant dictionnary
            ConcurrentDictionary<string, IList<int>> shuffled = new ConcurrentDictionary<string, IList<int>>();
            
            foreach(KeyValuePair<string, int> word in WordValPair)
            {
                // If the key is known add a unite
                if (shuffled.ContainsKey(word.Key))
                {
                    shuffled[word.Key].Add(1);
                }
                else
                {
                    IList<int> count = new List<int>();
                    count.Add(1);
                    shuffled.TryAdd(word.Key, count);
                }
                // I tried to parallelize the counting but even with a multithreading resistant dictionnary some values 
                // were missing so I kept the normal method.
            }

            return shuffled;


        }
        
        //Make the count foreach word in the Shuffled list
        static ConcurrentDictionary<string, int> Reduce(ConcurrentDictionary<string, IList<int>> list)
        {
            //Create the Word/Count result list.
            ConcurrentDictionary<string, int> result = new ConcurrentDictionary<string, int>();



            //from (word,(1,1,...,1))
            Parallel.ForEach(list, (pair) =>
            {
            int sum = 0;
                Parallel.ForEach(pair.Value, (value)=>
                 {
                    sum += value;
                 });

                //to (word,count)
                result.TryAdd(pair.Key, sum);
            });
            return result;
        }

        //Print the words_count dictionnary ordered by Keys for a better view
        public override string ToString()
        {
            SortedDictionary<string, int> words = new SortedDictionary<string, int>();
            foreach(KeyValuePair<string,int> entry in words_count)
            {
                words.Add(entry.Key,entry.Value);
            }
            
            string result = "";
            foreach(KeyValuePair<string, int> pair in words)
            {
                result += string.Format("{0}: {1} \n", pair.Key, pair.Value);
            }
            return result;
        }

    }

    
}

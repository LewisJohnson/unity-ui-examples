/* 
MIT License

Copyright (c) 2017 Lewis Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Common.Scripts {

    [AddComponentMenu("Scripts/Common/Extention Methods")]
    public static class ExtentionMethods {

        public static IEnumerable<IList<T>> Chunks<T>(this IEnumerable<T> xs, int size) {
            List<T> curr = new List<T>(size);

            foreach (T x in xs) {
                curr.Add(x);
                if (curr.Count != size) {
                    continue;
                }

                yield return curr;
                curr = new List<T>(size);
            }
        }

        public static IEnumerable<T[]> ChunksAsArray<T>(this IEnumerable<T> xs, int size) {
            T[] curr = new T[size];

            int i = 0;

            foreach (T x in xs) {
                curr[i % size] = x;

                if (++i % size != 0) {
                    continue;
                }

                yield return curr;
                curr = new T[size];
            }
        }

        public static int RoundToTenth(this int i) {
            return (int)Math.Round(i / 10.0) * 10;
        }
    }

}
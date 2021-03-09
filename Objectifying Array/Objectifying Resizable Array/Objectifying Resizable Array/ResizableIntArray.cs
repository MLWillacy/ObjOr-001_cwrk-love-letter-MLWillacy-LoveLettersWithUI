using System;
using System.Collections.Generic;
using System.Text;

namespace Objectifying_Resizable_Array
{
    class ResizableIntArray
    {
        int[] mStorage = new int[10];
        int mCurrentIndex = 0;

        public void Add(int pNumber)
        {
            mStorage[mCurrentIndex] = pNumber;
            mCurrentIndex++;

            if (IsFull())
            {
                ExpandStorage(10);
            }
        }

        public void ExpandStorage(int pSizeIncrease)
        {
            int[] increasedStorage = new int[mCurrentIndex + pSizeIncrease];
            for (int i = 0; i < mCurrentIndex; i++)
            {
                increasedStorage[i] = mStorage[i];
            }
            mStorage = increasedStorage;
        }

        public bool IsFull()
        {
            if (mCurrentIndex < mStorage.Length)
            {
                return false;
            }
            return true;
        }

        public void WriteContentsToConsole()
        {
            for (int i = 0; i < mCurrentIndex; i++)
            {
                Console.WriteLine(mStorage[i]);
            }
        }
        
        public int Average()
        {
            int total = 0;
            for (int i = 0; i < mCurrentIndex; i++)
            {
                total = total + mStorage[i];
            }
            return total / mCurrentIndex;
        }

        public int Max()
        {
            int highest = mStorage[0];
            for (int i = 1; i < mCurrentIndex; i++)
            {
                    if (mStorage[i] > mStorage[i - 1])
                    { highest = mStorage[i]; } 
            }
            return highest;

        }

        public int Min()
        {
            int lowest = mStorage[0];
            for (int i = 1; i < mCurrentIndex; i++)
            {
                    if (mStorage[i] < mStorage[i - 1])
                    { lowest = mStorage[i]; }
            }
            return lowest;

        }

        public void BubbleSort()
        {
            int numSwaps = 0;
            bool repeat = true;

            while(repeat)
            {
                for (int i = 1; i < mCurrentIndex; i++)
                {
                    if (mStorage[i] < mStorage[i - 1])
                    {
                        int temp = mStorage[i - 1];
                        mStorage[i - 1] = mStorage[i];
                        mStorage[i] = temp;
                        numSwaps++;
                    }
                }
                if (numSwaps == 0)
                { repeat = false; }
                else
                { numSwaps = 0; }
            }
        }

    }
}

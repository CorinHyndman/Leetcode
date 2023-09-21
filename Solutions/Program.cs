using System.Diagnostics.CodeAnalysis;

int[] ints = { 1, 0, 2, 3, 4 };
Solution.MaxChunksToSorted(ints);


public class Solution
{
    #region 1. Two Sum
    public int[] TwoSum(int[] nums, int target)
    {
        int length = nums.Length;
        int[] result = new int[2];
        for (int i = 0; i < length - 1; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    result[0] = i;
                    result[1] = j;
                }
            }
        }
        return result;
    }
    #endregion
    #region 3. Longest Substring Without Repeating Characters
    public int LengthOfLongestSubstring(string s)
    {
        int maxSubstringLength = 0;
        Queue<char> chars = new();
        foreach (char c in s)
        {
            while (chars.Contains(c))
            {
                chars.Dequeue();
            }
            chars.Enqueue(c);
            if (chars.Count > maxSubstringLength)
            {
                maxSubstringLength = chars.Count;
            }
        }
        return maxSubstringLength;
    }
    #endregion
    #region 4. Median of Two Sorted Arrays
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        List<int> nums = nums1.Concat(nums2).ToList();
        nums.Sort();

        int midpoint = nums.Count / 2;
        double median = nums.Count % 2 is 0
            ? (nums[midpoint] + nums[midpoint - 1]) / 2d
            :  nums[midpoint];

        return median;
    }
    #endregion
    #region 6. Zigzag Conversion
    public string Convert6(string s, int numRows)
    {
        if (numRows is 1)
        {
            return s;
        }

        int index = 0;
        int direction = 1;
        string[] rows = new string[numRows];
        for (int i = 0; i < s.Length; i++)
        {
            rows[index] += s[i];
            index += direction;
            if (index == 0 || index == numRows - 1)
            {
                direction *= -1;
            }
        }
        string stringZigzagged = string.Empty;
        foreach (string a in rows)
        {
            stringZigzagged += a;
        }
        return stringZigzagged;
    }
    #endregion
    #region 7. Reverse Integer
    public int Reverse(int x)
    {
        char[] digits = x.ToString()
            .Reverse()
            .Where(x => char.IsDigit(x))
            .ToArray();

        string s = x < 0 ? "-" : string.Empty;
        foreach (char c in digits)
        {
            s += c;
        }

        int.TryParse(s, out int result);
        return result;
    }
    #endregion
    #region 9. Palidrome
    public bool IsPalindrome(int x)
    {
        bool valid = true;
        string input = x.ToString();
        int length = input.Length;
        for (int i = 0; i < length / 2; i++)
        {
            if (input[i] != input[length - (i + 1)])
            {
                valid = false;
            }
        }
        return valid;
    }
    #endregion
    #region 10. Regular Expression Matching
    //public bool IsMatch(string s, string p)
    //{
    //}
    #endregion
    #region 11. Container With Most Water
    public int MaxArea(int[] height)
    {
        int i = 0;
        int j = height.Length - 1;

        int maxVol = 0;
        while (i != j)
        {
            maxVol = Math.Max(Math.Min(height[i], height[j]) * (j - i), maxVol);
            if (height[i] > height[j])
            {
                j--;
            }
            else
            {
                i++;
            }
        }
        return maxVol;
    }
    #endregion
    #region 13. Roman to Integer
    public int RomanToInt(string s)
    {
        Dictionary<char, int> romanNumeral = new()
        {
            { 'I', 1    },
            { 'V', 5    },
            { 'X', 10   },
            { 'L', 50   },
            { 'C', 100  },
            { 'D', 500  },
            { 'M', 1000 },
        };

        int total = 0;
        (int current, int next) value = new();
        for (int i = 0; i < s.Length; i++)
        {
            value.current = romanNumeral[s[i]];
            if (i + 1 < s.Length)
            {
                value.next = romanNumeral[s[i + 1]];
            }

            if (value.current < value.next && value.next <= value.current * 10)
            {
                total += value.next - value.current;
                i++;
            }
            else
            {
                total += value.current;
            }
            value.next = 0;
        }
        return total;
    }
    #endregion
    #region 14. Longest Common Prefix
    public string LongestCommonPrefix(string[] strs)
    {
        Array.Sort(strs);
        string prefix = strs[0];
        while (!strs.All(x => x.StartsWith(prefix)) && prefix is not "")
        {
            prefix = prefix[..^1];
        }
        return prefix;
    }
    #endregion
    #region 17. Letter Combinations of a Phone Number
    public IList<string> LetterCombinations(string digits)
    {
        Dictionary<char, string> valuePairs = new()
        {
            {'7', "pqrs"},
            {'8', "tuv" },
            {'9', "wxyz"},
        };

        for (int i = 0; i < 5; i++)
        {
            string numpad = string.Empty;
            numpad += (char)((i * 3) + 0 + 'a');
            numpad += (char)((i * 3) + 1 + 'a');
            numpad += (char)((i * 3) + 2 + 'a');
            valuePairs.Add((char)(i + '2'), numpad);
        }

        List<string> combinations = new() {""};
        for (int i = 0; i < digits.Length; i++)
        {
            string chars = valuePairs[digits[i]];
            int newIterations = chars.Length;

            List<string> newCombinations = new(combinations);
            for (int j = 0; ++j < newIterations;)
            {
                combinations.AddRange(newCombinations);
            }

            int cutoff = combinations.Count / newIterations;
            for (int j = 0; j < combinations.Count; j++)
            {
                combinations[j] += chars[j / cutoff];
            }
        }

        if (combinations.Count is 1)
        {
            combinations.Clear();
        }

        return combinations;
    }
    #endregion
    #region 20. Valid Parentheses
    public bool IsValid(string s)
    {
        bool valid = true;
        Stack<char> stack = new();
        Dictionary<char, char> parenthesesPairs = new()
        {
            { '(' , ')' },
            { '{' , '}' },
            { '[' , ']' } ,
        };
        foreach (char c in s)
        {
            if (parenthesesPairs.ContainsKey(c))
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count is 0 || parenthesesPairs[stack.Pop()] != c)
                {
                    valid = false;
                }
            }
        }
        return valid && stack.Count is 0;
    }
    #endregion
    #region 21. Merge Two Sorted Lists
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        List<int> nums = new List<int>();
        while (list1 is not null)
        {
            nums.Add(list1.val);
            list1 = list1.next;
        }
        while (list2 is not null)
        {
            nums.Add(list2.val);
            list2 = list2.next;
        }
        nums.Sort();

        ListNode current = null;
        ListNode previous = null;
        for (int i = nums.Count - 1; i >= 0; i--)
        {
            current = new ListNode(nums[i], previous);
            previous = current;
        }
        return current;
    }
    #endregion
    #region 26. Remove Duplicates from Sorted Array
    public int RemoveDuplicates(int[] nums)
    {
        int index = 1;
        int currentVal = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != currentVal)
            {
                nums[index] = nums[i];
                currentVal = nums[i];
                index++;
            }
        }
        return index;
    }
    #endregion
    #region 28. Find the Index of the First Occurrence in a String
    public int StrStr(string haystack, string needle)
    {
        Queue<char> queue = new();
        for (int i = 0; i < haystack.Length; i++)
        {
            queue.Enqueue(haystack[i]);
            if (queue.ToArray().SequenceEqual(needle))
            {
                return i - (queue.Count - 1);
            }
            if (queue.Count >= needle.Length)
            {
                queue.Dequeue();
            }
        }
        return -1;
    }
    #endregion
    #region 35. Search Insert Position
    public int SearchInsert(int[] nums, int target)
    {
        int index = 0;
        int lowerBound = 0;
        int upperBound = nums.Length;
        bool searching = true;
        while (searching)
        {
            index = (upperBound + lowerBound + 1) / 2;
            if (index is not 0 && nums[index - 1] < target && target < nums[index])
            {
                searching = false;
            }
            else if (nums[index] < target)
            {
                lowerBound = index;
            }
            else if (nums[index] > target)
            {
                upperBound = index;
            }
            else
            {
                searching = false;
            }
        }
        return index;
    }
    #endregion
    #region 50. Pow(x, n)
    //public double MyPow(double x, int n)
    //{ 

    //}
    #endregion
    #region 58. Length of Last Word
    public int LengthOfLastWord(string s) => 
         s.Trim().Split(' ').Last().Length;
    #endregion
    #region 67. Add Binary
    public string AddBinary(string a, string b)
    {
        if (a.Length < b.Length)
        {
            a = new string('0', b.Length - a.Length) + a;
        }
        else
        {
            b = new string('0', a.Length - b.Length) + b;
        }

        int i = a.Length - 1;
        bool carry = false;
        string result = "";
        while (i >= 0 || carry)
        {
            int total = Convert.ToInt32(carry);
            if (i >= 0)
            {
                total +=
                    a[i] - '0' +
                    b[i] - '0';
            }

            char currentResult = (total is 1 || total is 3)
                ? '1'
                : '0';

            result = currentResult + result;
            carry = total > 1;
            i--;
        }
        return result;
    }
    #endregion
    #region 70. Climbing Stairs
    public int ClimbStairs(int n)
    {
        int a = 0;
        int b = 1;
        int c = 0;
        for (int i = 0; i < n; i++)
        { 
            c = b + a;
            a = b;
            b = c;
        }
        return c;
    }
    #endregion
    #region 88. Merge Sorted Array
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        for (int i = 0; i < nums2.Length; i++)
        {
            for (int j = 0; j < nums1.Length; j++)
            {
                if (nums2[i] < nums1[j] || (j >= m + i))
                {
                    for (int k = nums1.Length; --k > j;)
                    {
                        nums1[k] = nums1[k - 1];
                    }
                    nums1[j] = nums2[i];
                    break;
                }
            }
        }
    }
    #endregion
    #region 190. Reverse Bits
    public uint reverseBits(uint n)
    {
        long reverseValue = 0;
        long bitmask;
        for (int i = 0; i < 32; i++)
        {
            bitmask = 1;
            bitmask <<= i;
            bitmask &= n;
            bitmask <<= 63 - (i+i);
            reverseValue |= bitmask;
        }
        reverseValue >>= 32;
        return (uint)reverseValue;
    }
    #endregion
    #region 191. Numbers of 1 Bits
    public int HammingWeight(uint n)
    {
        int numOnes = 0;
        long bitmask;
        for (int i = 0; i < 32; i++)
        {
            bitmask = 1;
            bitmask <<= i;
            bitmask &= n;
            if (bitmask > 0)
            {
                numOnes++;
            }
        }
        return numOnes;
    }
    #endregion
    #region 206. Reverse Linked List
    public ListNode ReverseList(ListNode head)
    {
        List<int> nums = new List<int>();
        while (head is not null)
        {
            nums.Add(head.val);
            head = head.next;
        }
        nums.Reverse();

        ListNode current = null;
        ListNode previous = null;
        for (int i = nums.Count - 1; i >= 0; i--)
        {
            current = new ListNode(nums[i], previous);
            previous = current;
        }
        return current;
    }
    #endregion
    #region 231. Power Of 2
    public bool IsPowerOfTwo(int n)
    {
        double x = Math.Log(n) / Math.Log(2);
        x = Math.Round(x, 10);
        return (x % 1 is 0);
    }
    #endregion
    #region 242. Valid Anagram
    public bool IsAnagram(string s, string t)
    {
        char[] sChars = s.ToCharArray();
        char[] tChars = t.ToCharArray();
        Array.Sort(sChars);
        Array.Sort(tChars);
        return sChars.SequenceEqual(tChars);
    }
    #endregion
    #region 258. Add Digits
    public int AddDigits(int num)
    {
        string val = num.ToString();
        int total = num;
        while (val.Length > 1)
        {
            total = 0;
            foreach (char c in val)
            {
                total += c - '0';
            }
            val = total.ToString();
        }
        return total;
    }
    #endregion
    #region 326. Power of Three
    public bool IsPowerOfThree(int n)
    {
        double x = Math.Log(n) / Math.Log(3);
        x = Math.Round(x, 10);
        return (x % 1 is 0);
    }
    #endregion
    #region 342. Power of Four
    public bool IsPowerOfFour(int n)
    {
        double x = Math.Log(n) / Math.Log(4);
        x = Math.Round(x, 10);
        return (x % 1 is 0);
    }
    #endregion
    #region 409. Longest Palindrome
    public int LongestPalindrome(string s)
    {
        char[] c = s.ToCharArray();
        Array.Sort(c);

        int evenCount = 0;
        int largestOdd = 0;
        for (int i = 0; i < c.Length;)
        {
            int charCount = c.Where(x => x == c[i]).Count();
            if (charCount % 2 is 1)
            {
                if (charCount > largestOdd)
                {
                    if (largestOdd > 0)
                    {
                        evenCount += largestOdd - 1;
                    }
                    largestOdd = charCount;
                }
                else
                {
                    evenCount += charCount - 1;
                }
            }
            else
            {
                evenCount += charCount;
            }
            i += charCount;
        }
        return evenCount + largestOdd;
    }
    #endregion
    #region 441. Arranging Coins
    public int ArrangeCoins(int n)
    {
        int j = 0;
        for (int i = n; i > j; i -= j)
        {
            j++;
        }
        return j;
    }
    #endregion
    #region 704. Binary Search
    public int Search(int[] nums, int target)
    {
        int index = -1;
        int lower = 0;
        int upper = nums.Length - 1;
        while (lower <= upper)
        {
            int midpoint = (lower + upper) / 2;
            if (nums[midpoint] == target)
            {
                index = midpoint;
                lower = 1;
                upper = 0;
            }
            else if (nums[midpoint] > target)
            {
                upper = midpoint - 1;
            }
            else
            {
                lower = midpoint + 1;
            }
        }
        return index;
    }
    #endregion
    #region 771. Jewels and Stones
    public int NumJewelsInStones(string jewels, string stones)
    {
        int total = 0;
        foreach (char c in stones)
        {
            if (jewels.Contains(c))
            {
                total++;
            }
        }
        return total;
    }
    #endregion
    #region 769. Max Chunks To Make Sorted
    public static int MaxChunksToSorted(int[] arr)
    {
        List<List<int>> chunks = new();
        for (int i = 0; i < arr.Length; i++)
        {
            chunks.Add(new List<int>() 
            { 
                arr[i] 
            });
        }

        int count = chunks.Count;
        for (int i = 0; i < count; i++)
        {
            for (int j = chunks.Count; --j > 0;)
            {
                if (chunks[j].First() < chunks[j - 1].Last())
                {
                    chunks[j - 1].AddRange(chunks[j]);
                    chunks.RemoveAt(j);
                    chunks[j - 1].Sort();
                }
            }
        }

        return chunks.Count;
    }
    #endregion
    #region 941. Valid Mountain Array
    public bool ValidMountainArray(int[] arr)
    {
        int peak = Array.IndexOf(arr, arr.Max());
        if (peak == 0 || peak == arr.Length - 1)
        {
            return false;
        }
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (i < peak)
            {
                if (arr[i] >= arr[i + 1]) return false;
            }
            if (i >= peak)
            {
                if (arr[i] <= arr[i + 1]) return false;
            }
        }
        return true;
    }
    #endregion
    #region 1108. Defanging an IP Address
    public string DefangIPaddr(string address)
    {
        return address.Replace(".","[.]");
    }
    #endregion
    #region 1232. Check If It Is a Straight Line
    public bool CheckStraightLine(int[][] coordinates)
    {
        Array.Sort(coordinates, (x, y) => x[0].CompareTo(y[0]));
        double a = coordinates[0][0] - coordinates[1][0];
        double b = coordinates[0][1] - coordinates[1][1];
        double angle = Math.Atan(a / b);
        bool straightLine = true;

        for (int i = 2; i < coordinates.Length; i++)
        {
            a = coordinates[0][0] - coordinates[i][0];
            b = coordinates[0][1] - coordinates[i][1];

            if (angle != Math.Atan(a / b))
            {
                straightLine = false;
            }
        }
        return straightLine;
    }
    #endregion
    #region 1337. The K Weakest Rows in a Matrix
    public int[] KWeakestRows(int[][] mat, int k)
    {
        int rows = mat.Length;
        int cols = mat[0].Length;
        List<(int,int soldiers)> allRows = new();
        for (int i = 0; i < rows; i++)
        {
            int totalSoldiers = 0;
            for (int j = 0; j < cols && mat[i][j] is 1; j++)
            {
                totalSoldiers++;
            }
            allRows.Add((i,totalSoldiers));
        }
        (int index,int)[] weakestRows = allRows.OrderBy(x => x.soldiers).ToArray();
        int[] indexes = new int[k];
        for (int i = 0; i < k; i++)
        {
            indexes[i] = weakestRows[i].index;
        }
        return indexes;
    }
    #endregion
    #region 1662. Check If Two String Arrays are Equivalent
    public bool ArrayStringsAreEqual(string[] word1, string[] word2)
    {
        string a = string.Empty;   
        string b = string.Empty;
        foreach (string s in word1)
        {
            a += s;
        }
        foreach (string s in word2)
        {
            b += s;
        }
        return a == b;
    }
    #endregion
    #region 1812. Determine Color of a Chessboard Square
    public bool SquareIsWhite(string coordinates)
    {
        int col = coordinates[0] % 2;
        int row = coordinates[1] % 2;
        return col + row is 1;
    }
    #endregion
    #region 2824. Count Pairs Whose Sum is Less than Target
    public int CountPairs(IList<int> nums, int target)
    {
        int pairs = 0;
        int length = nums.Count;
        for (int i = 0; i < length; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                if (nums[i] + nums[j] < target)
                {
                    pairs++;
                }
            }
        }
        return pairs;
    }
    #endregion
}
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null) 
    { 
        this.val = val;
        this.next = next;
    }
 }
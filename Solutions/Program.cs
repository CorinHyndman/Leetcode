using System;
using System.Numerics;
using System.Reflection.Metadata;
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
    #region 2. Add Two Numbers
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        List<int> a = new();
        List<int> b = new();
        while (l1 is not null)
        {
            a.Add(l1.val);
            l1 = l1.next;
        }
        while (l2 is not null)
        {
            b.Add(l2.val);
            l2 = l2.next;
        }
        a.Reverse();
        b.Reverse();

        BigInteger aTotal = BigInteger.Parse(string.Join("", a.ConvertAll(x => x.ToString())));
        BigInteger bTotal = BigInteger.Parse(string.Join("", b.ConvertAll(x => x.ToString())));
        BigInteger total = aTotal + bTotal;

        int[] c = Array.ConvertAll(total.ToString().ToCharArray(), x => x - '0');

        ListNode head = null;
        for (int i = 0; i < c.Length; i++)
        {
            head = new(c[i], head);
        }
        return head;
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
            : nums[midpoint];

        return median;
    }
    #endregion
    //#region 5. Longest Palindromic Substring
    public static string LongestPalindromeSubstring(string s)   ///////////////////
    {
        s = $" {s} ";
        int left, right;
        string longest = s[1..2];
        string subString = string.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            int remaining = s.Length - i;
            for (int j = 1; j < Math.Min(i, remaining); j++)
            {
                left = i - j;
                right = i + j;
                if (s[left] != s[right])
                {
                    subString = s[left..right];

                    if (subString.First() != subString.Last())
                    {
                        if (subString.First() == subString[^2])
                        {
                            subString = subString[..^1];
                        }
                        else
                        {
                            subString = subString[1..];
                        }
                    }
                    longest = subString.Length > longest.Length
                        ? subString
                        : longest;
                    break;
                }

                subString = s[left..(right + 1)];
                longest = subString.Length > longest.Length
                    ? subString
                    : longest;

            }
        }
        return longest;
    }
    //#endregion
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
    #region 8. String to Integer (atoi)
    public static int MyAtoi(string s)
    {
        s = s.Trim();

        int sign = 0;
        int result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (i is 0 && (s[i] is '+' or '-'))
            {
                sign = s[i] is '-'
                    ? -1
                    : 1;
                continue;
            }
            else
            {
                if (!char.IsDigit(s[i]))
                {
                    break;
                }
            }

            if ((result * 10L) + s[i] - '0' > int.MaxValue)
            {
                return sign is -1
                    ? int.MinValue
                    : int.MaxValue;
            }

            result *= 10;
            result += s[i] - '0';
        }

        if (sign is not 0)
        {
            result *= sign;
        }

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
    #region 27. Remove Element
    public static int RemoveElement(int[] nums, int val)
    {
        int count = nums.Where(x => x == val).Count();
        int right = nums.Length - 1;
        for (int i = 0; i < right; i++)
        {
            if (nums[i] == val)
            {
                while (nums[right] == val && right > 0)
                {
                    right--;
                }
                nums[i] = nums[right];
                count++;
                right--;
            }
        }
        return nums.Length - count;
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
    #region 30. Substring with Concatenation of All Words
    public IList<int> FindSubstring(string s, string[] words)
    {
        int wordLength = words[0].Length;
        int wordsLength = words.Length;
        int substringLength = wordLength * wordsLength;
        if (substringLength > s.Length)
        {
            return new List<int>();
        }

        List<int> list = new();
        string word = string.Join(string.Empty, words);
        for (int i = 0; i <= s.Length - substringLength; i++)
        {
            string currentSubstring = s[i..(i + substringLength)];
            if (word != currentSubstring)
            {
                List<string> remainingWords = words.ToList();
                int endIndex = currentSubstring.Length - wordLength;
                for (int j = 0; j <= endIndex; j += wordLength)
                {
                    remainingWords.Remove(currentSubstring[j..(j + wordLength)]);
                }

                if (remainingWords.Count is 0)
                {
                    list.Add(i);
                }
            }
            else
            {
                list.Add(i);
            }
        }
        return list;
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
    #region 42. Trapping Rain Water
    public static int Trap(int[] height)
    {
        if (height.Length <= 1)
        {
            return 0;
        }

        int firstIndex = 0;
        int lastIndex = 0;
        for (int i = 0; i + 1 < height.Length; i++)
        {
            if (height[i + 1] < height[i])
            {
                firstIndex = i;
                break;
            }
        }
        for (int i = height.Length; i-- > 0;)
        {
            if (height[i - 1] < height[i])
            {
                lastIndex = i;
                break;
            }
        }
        height = height[firstIndex..(lastIndex + 1)];

        int max = 0;
        int total = 0;
        List<int> heights = new(height);
        int leftIndex = heights.IndexOf(heights.Max());
        int rightIndex = leftIndex;
        while (leftIndex > 0)
        {
            max = height[..rightIndex].Max();

            leftIndex--;
            if (height[leftIndex] == max)
            {
                rightIndex = leftIndex;
            }
            else
            {
                total += max - height[leftIndex]; 
            }
        }

        max = 0;
        leftIndex = heights.IndexOf(heights.Max()) + 1;
        rightIndex = leftIndex + 1;
        while (rightIndex < height.Length)
        {
            max = height[leftIndex..].Max();

            if (height[rightIndex] == max)
            {
                leftIndex = rightIndex;
            }
            else
            {
                total += max - height[rightIndex];
            }
            rightIndex++;
        }
        return total;
    }
    #endregion
    #region 50. Pow(x, n)
    public static double MyPow(double x, int n)
    {
        if (n is 0 || Math.Round(x,8) is 1)
        {
            return 1;
        }
        if(x is -1)
        {
            return n % 2 is 0 ? -1 : 1;
        }

        double power = Math.Abs(n * 1d);
        double xStart = x;
        for (int i = 1; i < power; i++)
        {
            x *= xStart;
            if (double.IsInfinity(x))
            {
                return 0;
            }
        }

        if (n < 0)
        {
            x = 1 / x;
        }

        return x;
    }
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
    #region 69. Sqrt(x)
    public int MySqrt(int x)
    {        
        for (long i = 1; i < int.MaxValue; i++)
        {
            if (i * i > x)
            {
                return (int)(i - 1);
            }
        }
        return 0;
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
    #region 73. Set Matrix Zeroes
    public static void SetZeroes(int[][] matrix)
    {
        int row = matrix.Length;
        int col = matrix[0].Length;
        List<(int row, int col)> zeros = new();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (matrix[i][j] is 0)
                {
                    zeros.Add((i,j));
                }
            }
        }

        foreach ((int r, int c) in zeros)
        {
            matrix[r] = new int[col]; 
            for (int i = 0; i < row; i++)
            {
                matrix[i][c] = 0;
            }
        }
    }
    #endregion
    #region 75. Sort Colors
    public void SortColors(int[] nums)
    {
        int tmp;
        bool swap;
        for (int i = 0; i < nums.Length; i++)
        {
            swap = false;
            for (int j = 0; j + 1 < nums.Length; j++)
            {
                if (nums[j] > nums[j + 1])
                {
                    tmp = nums[j];
                    nums[j] = nums[j + 1];
                    nums[j + 1] = tmp;
                    swap = true;
                }
            }
            if (!swap)
            {
                return;
            }
        }
    }
    #endregion
    #region 83. Remove Duplicates from Sorted List
    public ListNode DeleteDuplicates(ListNode head)
    {
        ListNode current = head;
        while (current is not null)
        {
            current.next = GetNextNonDuplicate(current, current.val);
            current = current.next;
        }
        return head;

        ListNode GetNextNonDuplicate(ListNode node, int val)
        {
            if (node.val == val)
            {
                if (node.next is not null)
                {
                    return GetNextNonDuplicate(node.next, val);
                }
                return null;
            }
            return node;
        }
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
    #region 118. Pascal's Triangle
    public IList<IList<int>> Generate(int numRows)
    {
        List<List<int>> triangle = new()
        {
            new() { 1 }
        };

        for (int i = 1; i < numRows; i++)
        {
            List<int> currentRow = new();
            for (int j = 0; j <= i; j++)
            {
                int previousLeft = j - 1 >= 0 
                    ? triangle[i - 1][j - 1] 
                    : 0;

                int previousCurrent = j < triangle[i - 1].Count
                    ? triangle[i - 1][j]
                    : 0;
                currentRow.Add(previousCurrent + previousLeft);
            }
            triangle.Add(currentRow);
        }
        return triangle.ToArray();
    }
    #endregion
    #region 169. Majority Element
    public int MajorityElement(int[] nums)
    {
        int majority = nums.Length / 2;
        Array.Sort(nums);

        for (int i = 0; i + majority < nums.Length; i++)
        {
            if (nums[i] == nums[i + majority])
            {
                return nums[i];
            }
        }
        return 0;
    }
    #endregion
    #region 171. Excel Sheet Column Number
    public int TitleToNumber(string columnTitle)
    {
        int total = 0;
        int power = columnTitle.Length - 1;
        foreach (char c in columnTitle)
        {
            total += (c - 'A' + 1) * (int)Math.Pow(26, power);
            power--;
        }
        return total;
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
    #region 205. Isomorphic Strings
    public static bool IsIsomorphic(string s, string t)
    {
        Dictionary<char, char> newMapping = new();
        for (int i = 0; i < s.Length; i++)
        {
            if (newMapping.ContainsKey(s[i]))
            {
                if (newMapping[s[i]] != t[i])
                {
                    return false;
                }
            }
            else if (newMapping.ContainsValue(t[i]))
            {
                return false;
            }
            else
            {
                newMapping.Add(s[i], t[i]);
            }
        }
        return true;
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
    #region 257. Binary Tree Paths
    public IList<string> BinaryTreePaths(TreeNode root)
    {
        List<List<int>> allPaths = new();
        Stack<int> currentPath = new();

        GetPaths(root);

        List<string> paths = new();
        foreach (var list in allPaths)
        {
            paths.Add(string.Join("->", list));
        }
        return paths;

        void GetPaths(TreeNode node)
        {
            currentPath.Push(node.val);
            if (node.left is null && node.right is null)
            {
                allPaths.Add(currentPath.Reverse().ToList());
                currentPath.Pop();
                return;
            }

            if (node.right is not null)
            {
                GetPaths(node.right);
            }
            if (node.left is not null)
            {
                GetPaths(node.left);
            }
            currentPath.Pop();
        }
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
    #region 283. Move Zeroes
    public void MoveZeroes(int[] nums)
    {
        int tmp = 0;
        int length = nums.Length;
        for (int i = length; i-- > 0;)
        {
            if (nums[i] is 0)
            {
                for (int j = i; j + 1 < length; j++)
                {
                    tmp = nums[j];
                    nums[j] = nums[j + 1];
                    nums[j + 1] = tmp;
                }
            }
        }
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
    #region 345. Reverse Vowels of a String
    public string ReverseVowels(string s)
    {
        char[] vowel = { 'a', 'e', 'i', 'o', 'u' };
        char[] vowels = s.Where(x => vowel.Contains(x)).Reverse().ToArray();

        int a = 0;
        int b = s.Length - 1;
        var chars = s.ToList();
        while (a != b)
        {
            do
            {
                a++;
            } while (!vowels.Contains(chars[a]));
            
            do
            {
                b--;
            } while (!vowels.Contains(chars[b]));

            char tmp = chars[a];
            chars[a] = chars[b];
            chars[b] = tmp;
        }

        string d = string.Empty;
        foreach (char c in chars)
        {
            d += c;
        }
        return d;
    }
    #endregion
    #region 389. Find the Difference
    public char FindTheDifference(string s, string t)
    {
        var a = s.OrderBy(x => x).ToList();
        var b = t.OrderBy(x => x).ToList();

        for (int i = 0; i < b.Count - 1; i++)
        {
            if (a[i] != b[i])
            {
                return b[i];
            }
        }
        return b.Last();
    }
    #endregion
    #region 392. Is Subsequence
    public bool IsSubsequence(string s, string t)
    {
        if (s == string.Empty)
        {
            return true;
        }
        if (t == string.Empty)
        {
            return false;
        }

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != t[i] && t.Length > s.Length)
            {
                t = t.Remove(i,1);
                i--;
            }
        }

        t = t[..s.Length];
        return t == s;
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
    #region 456. 132 Pattern
    public bool Find132pattern(int[] nums)
    {
        if (nums.Length < 3)
        {
            return false;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                for (int k = j + 1; k < nums.Length; k++)
                {
                    if (nums[i] < nums[k] && nums[k] < nums[j])
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    #endregion
    #region 557. Reverse Words in a String III
    public string ReverseWords(string s)
    {
        string[] words = s.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = string.Join("", words[i].Reverse());
        }
        return string.Join(" ", words);
    }
    #endregion
    #region 605. Can Place Flowers
    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        int count = 0;
        bool planted = true;
        while (planted)
        {
            planted = false;
            for (int i = 0; i < flowerbed.Length; i++)
            {
                bool validPlant = true;
                validPlant = validPlant && (flowerbed[i] is 0);
                validPlant = validPlant && (i - 1 == -1 || flowerbed[i - 1] is 0);
                validPlant = validPlant && (i + 1 >= flowerbed.Length || flowerbed[i + 1] is 0);

                if (validPlant)
                { 
                    flowerbed[i] = 1;
                    planted = true;
                    count++;
                }
            }
        }
        return count >= n;
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
    public int MaxChunksToSorted(int[] arr)
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
    #region 896. Monotonic Array
    public  static bool IsMonotonic(int[] nums)
    {
        Func<int, int, bool> comparison = nums.Max() > nums[0]
            ? (i, j) => i <= j  // Increasing
            : (i, j) => i >= j; // Decreasing

        for (int i = 0; i + 1 < nums.Length; i++)
        {
            if (!comparison(nums[i], nums[i + 1]))
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #region 905. Sort Array By Parity
    public int[] SortArrayByParity(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;
        while (left <= right)
        {
            bool leftOdd = nums[left] % 2 is 1;
            bool rightEven = nums[right] % 2 is 0;
            if (leftOdd && rightEven)
            {
                int tmp = nums[right];
                nums[right] = nums[left];
                nums[left] = tmp;
            }

            if (leftOdd)
            {
                right--;
            }
            else if (rightEven)
            {
                left++;
            }
            else
            {
                right--;
                left++;
            }
        }
        return nums;
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
    #region 1137. N-th Tribonacci Number
    public int Tribonacci(int n)
    {
        List<int> sequence = new(){ 0,1,1 };
        for (int i = 2; i < n; i++)
        {
            sequence.Add(sequence.Sum());
            sequence.RemoveAt(0);
        }
        return n < 3 ? sequence[n] : sequence.Last();
    }
    #endregion
    #region 1207. Unique Number of Occurrences
    public bool UniqueOccurrences(int[] arr)
    {
        int[] distinctArr = arr.Distinct().ToArray();
        List<int> counts = new();
        foreach (int i in distinctArr)
        {
            counts.Add(arr.Count(x => x == i));
        }
        return counts.Count() == counts.Distinct().Count();
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
    #region 1431. Kids With the Greatest Number of Candies
    public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        int max = candies.Max();
        bool[] overMax = new bool[candies.Length];
        for (int i = 0; i < candies.Length; i++)
        {
            overMax[i] = candies[i] + extraCandies >= max;
        }
        return overMax;
    }
    #endregion
    #region 1512. Number of Good Pairs
    public int NumIdenticalPairs(int[] nums)
    {
        int count = 0;
        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] == nums[j])
                {
                    count++;
                }
            }
        }
        return count;
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
    #region 1768. Merge Strings Alternately
    public string MergeAlternately(string word1, string word2)
    {
        int length = Math.Min(word1.Length, word2.Length);
        string mergedWord = string.Empty;
        for (int i = 0; i < length; i++)
        {
            mergedWord += word1[i];
            mergedWord += word2[i];
        }
        mergedWord += word1[length..];
        mergedWord += word2[length..];
        return mergedWord;
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
    #region 2164. Sort Even and Odd Indices Independently
    public static int[] SortEvenOdd(int[] nums)
    {
        for (int j = 0; j < nums.Length; j++)
        {
            bool even = true;
            for (int i = 0; i + 2 < nums.Length; i++)
            {
                int next = i + 2;
                if (even)
                {
                    if (nums[i] > nums[next])
                    {
                        int tmp = nums[i];
                        nums[i] = nums[next];
                        nums[next] = tmp;
                    }
                }
                else
                {
                    if (nums[i] < nums[next])
                    {
                        int tmp = nums[i];
                        nums[i] = nums[next];
                        nums[next] = tmp;
                    }
                }
                even = !even;
            }
        }
        return nums;
    }
    #endregion
    #region 2771. Longest Non-decreasing Subarray From Two Arrays
    public static int MaxNonDecreasingLength(int[] nums1, int[] nums2)
    {
        int[] nums3 = new int[nums1.Length];        
        for (int i = 0; i < nums3.Length; i++)
        {
            int previous = 0;
            if (i > 0)
            {
                previous = nums3[i - 1];
            }

            int min = Math.Min(nums1[i], nums2[i]);
            int max = Math.Max(nums1[i], nums2[i]);
            if (i is 0 || previous > max)
            {
                nums3[i] = min;
                continue;
            }

            nums3[i] = min >= previous ? min : max;
        }


        int longest = 1;
        {
            int count = 1;
            for (int i = 0; i < nums3.Length - 1; i++)
            {
                if (nums3[i] <= nums3[i + 1])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }
                longest = Math.Max(longest, count);
            }
        }
        {
            int count = 1;
            for (int i = 0; i < nums2.Length - 1; i++)
            {
                if (nums2[i] <= nums2[i + 1])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }
                longest = Math.Max(longest, count);
            }
        }
        {
            int count = 1;
            for (int i = 0; i < nums1.Length - 1; i++)
            {
                if (nums1[i] <= nums1[i + 1])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }
                longest = Math.Max(longest, count);
            }
        }
        return longest;
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

#region Leetcode Classes
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
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
#endregion
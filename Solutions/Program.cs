using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
Solution.BuildArray(new int[] {1,3 },3);
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
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
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
    #region 5. Longest Palindromic Substring
    public string LongestPalindrome(string s)
    {
        int l, r, size;
        int sLength = s.Length;
        string longestSubstring = s[0].ToString();
        for (int i = 0; i < s.Length; i++)
        {
            size = 1;
            for (int j = 1; j <= Math.Min(i, sLength - i - 1); j++)
            {
                l = i - j;
                r = i + j;
                if (s[l] == s[r])
                {
                    size += 2;
                    if (size > longestSubstring.Length)
                    {
                        longestSubstring = s[l..(r + 1)];
                    }
                }
                else
                {                    
                    break;
                }
            }

            size = 1;
            for (int j = 1; j <= Math.Min(i, sLength - i); j++)
            {
                l = i - j;
                r = i + j - 1;
                if (s[l] == s[r])
                {
                    size += 2;
                    if (size > longestSubstring.Length)
                    {
                        longestSubstring = s[l..(r + 1)];
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return longestSubstring;

        //char currentChar;
        //string subString;
        //string longestSubstring = s[0].ToString();
        //for (int i = 0; i < s.Length; i++)
        //{
        //    currentChar = s[i];
        //    for (int j = s.Length - 1; i + j >= longestSubstring.Length && j > i; j--)
        //    {
        //        if (s[j] == currentChar)
        //        {
        //            subString = s[i..(j + 1)];
        //            if (subString.Length > longestSubstring.Length &&
        //                subString == string.Join("",subString.Reverse()))
        //            {
        //                longestSubstring = subString;
        //                break;
        //            }
        //        }
        //    }
        //}
        //return longestSubstring;
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
    #region 8. String to Integer (atoi)
    public int MyAtoi(string s)
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
    #region 12. Integer to Roman
    public string IntToRoman(int num)
    {
        Dictionary<int, char> romanNumerals = new()
        {
            {1 ,   'I'},
            {5 ,   'V'},
            {10,   'X'},
            {50,   'L'},
            {100,  'C'},
            {500,  'D'},
            {1000, 'M'},
        };

        string romanNumeral = string.Empty;
        int currentNum, realValue;        
        int currentPow = 1;
        while (num > 0)
        {
            currentNum = num % 10;

            realValue = currentNum * currentPow;
            if (romanNumerals.ContainsKey(realValue))
            {
                romanNumeral = romanNumerals[realValue] + romanNumeral;
            }
            else
            {
                if (currentNum is 4 or 9)
                {
                    romanNumeral = String.Join("", romanNumerals[currentPow], romanNumerals[(currentNum + 1) * currentPow], romanNumeral);
                }
                else
                {
                    for (int i = 0; i < currentNum % 5; i++)
                    {
                        romanNumeral = romanNumerals[currentPow] + romanNumeral;
                    }
                    if (currentNum > 5)
                    {
                        romanNumeral = romanNumerals[5 * currentPow] + romanNumeral;
                    }
                }
            }
            currentPow *= 10;
            num /= 10;
        }
        return romanNumeral;
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

        List<string> combinations = new() { "" };
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
    #region 19. Remove Nth Node From End of List
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        List<ListNode> nodes = new();
        while (head != null)
        {
            nodes.Add(head);
            head = head.next;
        }

        int index = nodes.Count - n;
        if (index - 1 >= 0 && index + 1 < nodes.Count)
        {
            nodes[index - 1].next = nodes[index + 1];
        }

        nodes.RemoveAt(index);

        if (nodes.Count > 0)
        {
            nodes[^1].next = null;
        }

        return nodes.Count > 0 ? nodes[0] : null;
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
    #region 24. Swap Nodes in Pairs
    public ListNode SwapPairs(ListNode head)
    {
        List<ListNode> nodes = new();
        while (head is not null)
        {
            nodes.Add(head);
            head = head.next;
        }

        Console.WriteLine(nodes.Count);
        for (int i = 1; i < nodes.Count; i += 2)
        {
            var next = nodes[i - 1];
            nodes[i - 1] = nodes[i];
            nodes[i] = next;
        }
        for (int i = 0; i + 1 < nodes.Count; i++)
        {
            nodes[i].next = nodes[i + 1];
        }
        
        if (nodes.Count > 0)
        {
            nodes[^1].next = null;
            return nodes[0];
        }

        return null;
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
    public int RemoveElement(int[] nums, int val)
    {
        int count = nums.Count(x => x == val);
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
    #region 29. Divide Two Integers
    public int Divide(int dividend, int divisor)
    {
        bool negative = dividend < 0 ^ divisor < 0;
        long count = 0;
        long absDivisor = Math.Abs((long)divisor);
        long absDividend = Math.Abs((long)dividend);
        for (long i = absDividend; i >= absDivisor; i -= absDivisor)
        {
            count++;
        }

        if (count > int.MaxValue)
        {
            return negative ? int.MinValue :int.MaxValue;
        }
        return negative ? (int)-count : (int)count;
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
    #region 33. Search in Rotated Sorted Array
    public int SearchRotatedSortedArray(int[] nums, int target)
    {
        int pivot = 0;
        for (int i = 0; i + 1 < nums.Length; i++)
        {
            if (nums[i] > nums[i + 1])
            {
                pivot = i + 1;
                break;
            }
        }
        nums = nums[pivot..].Concat(nums[0..pivot]).ToArray();

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

        if (index is not -1)
        {
            index += pivot;
            index %= nums.Length;
        }
        return index;
    }
    #endregion
    #region 34. Find First and Last Position of Element in Sorted Array
    public int[] SearchRange(int[] nums, int target)
    {
        if (!nums.Contains(target))
        {
            return new int[] { -1, -1 };
        }

        int left = 0;
        int right = nums.Length;
        int middle = 0;
        bool found = false;
        while (!found)
        {
            middle = (left + right) / 2;
            if (nums[middle] == target)
            {
                found = true;
            }
            else if (nums[middle] < target)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }
        for (int i = middle; i - 1 >= 0; i--)
        {
            if (nums[i - 1] == target)
            {
                middle = i - 1;
            }
            else
            {
                i = 0;
            }
        }

        int length = nums.Count(x => x == target) - 1;
        return new int[] { middle, middle + length };
    }
    #endregion
    #region 35. Search Insert Position
    public int SearchInsert(int[] nums, int target)
    {
        var numList = nums.ToList();
        int index = numList.IndexOf(target);
        if (index is -1)
        {
            return numList.Count(x => x < target);
        }
        return index;
    }
    #endregion
    #region 36. Valid Sudoku
    public bool IsValidSudoku(char[][] board)
    {
        int boardSize = 9;
        int squareSize = 3;
        char currentChar;
        List<int> charsPresent = new();
        for (int i = 0; i < boardSize; i++)
        {
            charsPresent.Clear();
            for (int j = 0; j < boardSize; j++)
            {
                currentChar = board[i][j];
                if (charsPresent.Contains(currentChar))
                {
                    return false;
                }
                else if (currentChar != '.')
                {
                    charsPresent.Add(currentChar);
                }
            }

            charsPresent.Clear();
            for (int j = 0; j < boardSize; j++)
            {
                currentChar = board[j][i];
                if (charsPresent.Contains(currentChar))
                {
                    return false;
                }
                else if (currentChar != '.')
                {
                    charsPresent.Add(currentChar);
                }
            }
        }

        for (int row = 0; row < boardSize; row += squareSize)
        {
            for (int col = 0; col < boardSize; col += squareSize)
            {
                charsPresent.Clear();
                for (int i = row; i < row + squareSize; i++)
                {
                    for (int j = col; j < col + squareSize; j++)
                    {
                        currentChar = board[i][j];
                        if (charsPresent.Contains(currentChar))
                        {
                            return false;
                        }
                        else if (currentChar != '.')
                        {
                            charsPresent.Add(currentChar);
                        }
                    }
                }
            }
        }
        return true;
    }
    #endregion
    #region 38. Count and Say
    public string CountAndSay(int n)
    {
        string nums = "1";
        string nextNum = string.Empty;
        for (int i = 1; i < n; i++)
        {
            int count = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                count++;
                if (j + 1 >= nums.Length || nums[j] != nums[j + 1])
                {
                    nextNum += (char)(count + '0');
                    nextNum += nums[j];
                    count = 0;
                }
            }
            nums = nextNum;
            nextNum = string.Empty;
        }
        return nums;
    }
    #endregion
    #region 42. Trapping Rain Water
    public int Trap(int[] height)
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
    public double MyPow(double x, int n)
    {
        if (n is 0 || Math.Round(x, 8) is 1)
        {
            return 1;
        }
        if (x is -1)
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
    #region 66. Plus One
    public int[] PlusOne(int[] digits)
    {
        int index = digits.Length - 1;
        while (++digits[index] is 10)
        {
            digits[index] = 0;
            index--;

            if (index < 0)
            {
                int[] one = { 1 };
                return one.Concat(digits).ToArray();
            }
        }
        return digits;
    }
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
    public void SetZeroes(int[][] matrix)
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
                    zeros.Add((i, j));
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
    #region 81. Search in Rotated Sorted Array II
    public bool SearchRotatedSortedArrayII(int[] nums, int target)
    {
        Array.Sort(nums);
        return Array.BinarySearch(nums, target) >= 0;
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
    #region 101. Symmetric Tree
    public bool IsSymmetric(TreeNode root)
    {
        var leftSide = GetOrder(root.left, new List<int>(), true);
        var rightSide = GetOrder(root.right, new List<int>(), false);
        return leftSide.SequenceEqual(rightSide);

        List<int> GetOrder(TreeNode node, List<int> current, bool leftPriorty)
        {
            if (node is null)
            {
                current.Add(-1);
                return current;
            }

            current.Add(node.val);

            if (leftPriorty)
            {
                current = GetOrder(node.left, current, leftPriorty);
                current = GetOrder(node.right, current, leftPriorty);
            }
            else
            {
                current = GetOrder(node.right, current, leftPriorty);
                current = GetOrder(node.left, current, leftPriorty);
            }
            return current;
        }
    }
    #endregion
    #region 104. Maximum Depth of Binary Tree
    public int MaxDepth(TreeNode root)
    {
        return GetMaxDepth(root, 1);

        int GetMaxDepth(TreeNode node, int depth)
        {
            if (node is null)
            {
                return depth - 1;
            }
            return Math.Max(GetMaxDepth(node.left, depth + 1), GetMaxDepth(node.right, depth + 1));
        }
    }
    #endregion
    #region 112. Path Sum
    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root is null)
        {
            return false;
        }

        targetSum -= root.val;
        if (root.left is null && root.right is null && targetSum is 0)
        {
            return true;
        }

        return HasPathSum(root.left, targetSum) || HasPathSum(root.right, targetSum);
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
    #region 119. Pascal's Triangle II
    public IList<int> GetRow(int rowIndex)
    {
        List<List<int>> triangle = new()
        {
            new() { 1 }
        };

        for (int i = 1; i < rowIndex; i++)
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
        return triangle.Last();
    }
    #endregion
    #region 128. Longest Consecutive Sequence
    public int LongestConsecutive(int[] nums)
    {
        if (nums.Length < 2)
        {
            return nums.Length;
        }

        Array.Sort(nums);
        nums = nums.Distinct().ToArray();

        int count = 1;
        int max = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums.Contains(nums[i] + 1))
            {
                count++;
            }
            else
            {
                count = 1;
            }
            max = Math.Max(max, count);
        }
        return max;
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
            bitmask <<= 63 - (i + i);
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
    #region 202. Happy Number
    public bool IsHappy(int n)
    {
        int val;
        string nString;
        List<int> visitedNums = new();
        while (!visitedNums.Contains(n))
        {
            visitedNums.Add(n);
            nString = n.ToString();
            n = 0;
            for (int i = 0; i < nString.Length; i++)
            {
                val = nString[i] - '0';
                n += val * val;
            }

            if (n is 1)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region 205. Isomorphic Strings
    public bool IsIsomorphic(string s, string t)
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
    #region 222. Count Complete Tree Nodes
    public int CountNodes(TreeNode root)
    {
        if (root is null)
        {
            return 0;
        }

        return CountNodes(root.left) + CountNodes(root.right) + 1; 
    }
    #endregion
    #region 229. Majority Element II
    public IList<int> MajorityElementII(int[] nums)
    {
        List<int> values = new();
        int[] distinctNums = nums.Distinct().ToArray();
        foreach (int i in distinctNums)
        {
            if (nums.Count(x => x == i) > nums.Length / 3)
            {
                values.Add(i);
            }
        }
        return values;
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
    #region 263. Ugly Number
    public bool IsUgly(int n)
    {
        if (n <= 0)
        {
            return false;
        }

        foreach (int i in new int[] { 2,3,5 })
        {
            while (n % i is 0)
            {
                n /= i;
            }
        }
        return n is 1;
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
    #region 338. Counting Bits
    public int[] CountBits(int n)
    {
        int[] bits = new int[++n];
        for (int i = 0; i < n; i++)
        {
            bits[i] = Convert.ToString(i, 2).Count(x => x is '1');
        }
        return bits;
    }
    #endregion
    #region 342. Power of Four
    public bool IsPowerOfFour(int n)
    {
        double x = Math.Log(n) / Math.Log(4);
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
                t = t.Remove(i, 1);
                i--;
            }
        }

        t = t[..s.Length];
        return t == s;
    }
    #endregion
    #region 409. Longest Palindrome
    public int LongestPalindrome2(string s)
    {
        char[] c = s.ToCharArray();
        Array.Sort(c);

        int evenCount = 0;
        int largestOdd = 0;
        for (int i = 0; i < c.Length;)
        {
            int charCount = c.Count(x => x == c[i]);
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
    #region 501. Find Mode in Binary Search Tree
    public int[] FindMode(TreeNode root)
    {
        List<int> allNums = new();
        Traverse(root);

        ILookup<int,int> lookup = allNums.ToLookup(x => x);
        int modeNum = lookup.Max(x => x.Count());
        return lookup.Where(x => x.Count() == modeNum)
            .Select(x => x.Key)
            .ToArray();

        void Traverse(TreeNode node)
        {
            if (node is null)
            {
                return;
            }
            allNums.Add(node.val);
            Traverse(node.left);
            Traverse(node.right);
        }
    }
    #endregion
    #region 515. Find Largest Value in Each Tree Row
    public IList<int> LargestValues(TreeNode root)
    {
        List<int> max = new();
        SearchTree(root, 0);
        return max.ToArray();

        void SearchTree(TreeNode node, int depth)
        {
            if (node is null)
            {
                return;
            }

            if (max.Count <= depth)
            {
                max.Add(node.val);
            }
            else
            {
                max[depth] = Math.Max(node.val, max[depth]);
            }

            SearchTree(node.left, depth + 1);
            SearchTree(node.right, depth + 1);
        }
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
    #region 561. Array Partition
    public int ArrayPairSum(int[] nums)
    {
        Array.Sort(nums);
        int total = 0;
        for (int i = nums.Length - 2; i > 0; i -= 2)
        {
            total += nums[i];
        }
        return total;
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
    #region 728. Self Dividing Numbers
    public IList<int> SelfDividingNumbers(int left, int right)
    {
        bool valid;
        string numString;
        List<int> nums = new();
        for (int i = left; i <= right; i++)
        {
            numString = i.ToString();
            if (numString.Contains('0'))
            {
                continue;
            }

            valid = true;
            foreach (char c in i.ToString())
            {
                if (i % (c - '0') != 0)
                {
                    valid = false;
                }
            }
            if (valid)
            {
                nums.Add(i);
            }
        }
        return nums;
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
    #region 775. Global and Local Inversions
    public bool IsIdealPermutation(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (Math.Abs(i - nums[i]) > 1)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #region 779. K-th Symbol in Grammar
    public int KthGrammar(int n, int k)
    {
        List<int> treeTraversalOrder = new();

        int current = k;
        while (current != 1)
        {
            treeTraversalOrder.Add(current);
            if (current % 2 is 0)
            {
                current /= 2;
            }
            else
            {
                current++;
                current /= 2;
            }
        }
        treeTraversalOrder.Reverse();
        return FindKthNum(0,1,0,k);

        int FindKthNum(int row, int column, int value, int target)
        {
            if (column == target)
            {
                return value;
            }

            if (treeTraversalOrder[row] == column * 2)
            {
                return FindKthNum(row + 1, (column * 2), value is 1 ? 0 : 1, target);
            }
            else
            {
                return FindKthNum(row + 1, (column * 2) - 1, value, target);
            }
        }
    }
    #endregion
    #region 896. Monotonic Array
    public bool IsMonotonic(int[] nums)
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
    #region 1071. Greatest Common Divisor of Strings
    public string GcdOfStrings(string str1, string str2)
    {
        string shortString = str1.Length <= str2.Length ? str1 : str2;
        for (int i = shortString.Length; i > 0; i--)
        {
            if (str1.Replace(shortString[0..i], "") == string.Empty &&
                str2.Replace(shortString[0..i], "") == string.Empty)
            {
                return shortString[0..i];
            }
        }
        return string.Empty;
    }
    #endregion
    #region 1095. Find in Mountain Array
    public int FindInMountainArray(int target, MountainArray mountainArr)
    {
        int left = 0;
        int right = mountainArr.Length() - 1;
        int peak = 0;
        int num, numLeft, numRight, middle;
        while (left <= right)
        {
            middle = (left + right + 1) / 2;
            num = mountainArr.Get(middle);

            numLeft = int.MaxValue;
            if (middle - 1 >= 0)
            {
                numLeft = mountainArr.Get(middle - 1);
            }

            numRight = int.MaxValue;
            if (middle + 1 <= mountainArr.Length() - 1)
            {
                numRight = mountainArr.Get(middle + 1);
            }

            if (num > numLeft && num > numRight)
            {
                peak = middle;
                left = right + 1;
            }
            else if (numLeft < num && num < numRight)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        foreach ((int l, int r) in new (int, int)[] { (0, peak), (peak, mountainArr.Length() - 1) })
        {
            left = l;
            right = r;
            while (left <= right)
            {
                middle = (left + right + 1) / 2;
                num = mountainArr.Get(middle);

                numLeft = int.MaxValue;
                if (middle - 1 >= 0)
                {
                    numLeft = mountainArr.Get(middle - 1);
                }

                numRight = int.MaxValue;
                if (middle + 1 <= mountainArr.Length() - 1)
                {
                    numRight = mountainArr.Get(middle + 1);
                }

                if (num == target)
                {
                    return middle;
                }

                if (numLeft < target && target < numRight)
                {
                    left = right + 1;
                }
                else if (num < target)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
        }

        left = 0;
        right = mountainArr.Length() - 1;
        if (mountainArr.Get(left) == target)
        {
            return left;
        }
        if (mountainArr.Get(right) == target)
        {
            return right;
        }
        return -1;
    }
    #endregion
    #region 1108. Defanging an IP Address
    public string DefangIPaddr(string address)
    {
        return address.Replace(".", "[.]");
    }
    #endregion
    #region 1137. N-th Tribonacci Number
    public int Tribonacci(int n)
    {
        List<int> sequence = new() { 0, 1, 1 };
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
    #region 1281. Subtract the Product and Sum of Digits of an Integer
    public int SubtractProductAndSum(int n)
    {
        int sum = 0;
        int product = 1;
        string nString = n.ToString();
        foreach (char c in nString)
        {
            sum += c - '0';
            product *= c - '0';
        }
        return product - sum;
    }
    #endregion
    #region 1282. Group the People Given the Group Size They Belong To
    public IList<IList<int>> GroupThePeople(int[] groupSizes)
    {
        List<int>[] groups = new List<int>[groupSizes.Length + 1];
        for (int i = 0; i < groups.Length; i++)
        {
            groups[i] = new List<int>();
        }
        for (int i = 0; i < groupSizes.Length; i++)
        {
            groups[groupSizes[i]].Add(i);
        }

        List<List<int>> allGroups = new();
        for (int i = 1; i < groups.Length; i++)
        {
            for (int j = 0; j < groups[i].Count; j += i)
            {
                allGroups.Add(groups[i].GetRange(j, i));
            }
        }

        return allGroups.ToArray();
    }
    #endregion
    #region 1337. The K Weakest Rows in a Matrix
    public int[] KWeakestRows(int[][] mat, int k)
    {
        int rows = mat.Length;
        int cols = mat[0].Length;
        List<(int, int soldiers)> allRows = new();
        for (int i = 0; i < rows; i++)
        {
            int totalSoldiers = 0;
            for (int j = 0; j < cols && mat[i][j] is 1; j++)
            {
                totalSoldiers++;
            }
            allRows.Add((i, totalSoldiers));
        }
        (int index, int)[] weakestRows = allRows.OrderBy(x => x.soldiers).ToArray();
        int[] indexes = new int[k];
        for (int i = 0; i < k; i++)
        {
            indexes[i] = weakestRows[i].index;
        }
        return indexes;
    }
    #endregion
    #region 1356. Sort Integers by The Number of 1 Bits
    public int[] SortByBits(int[] arr)
    {
        return arr.OrderBy(x => bitCount(x)).ThenBy(x => x).ToArray();
        int bitCount(int num) => Convert.ToString(num,2).Count(x => x is '1');
    }
    #endregion
    #region 1365. How Many Numbers Are Smaller Than the Current Number
    public int[] SmallerNumbersThanCurrent(int[] nums)
    {
        int[] numCount = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            numCount[i] = nums.Count(x => x < nums[i]);
        }
        return numCount;
    }
    #endregion
    #region 1379. Find a Corresponding Node of a Binary Tree in a Clone of That Tree
    public TreeNode GetTargetCopy(TreeNode original, TreeNode cloned, TreeNode target)
    {
        return FindTargetValue(cloned, target.val);

        TreeNode FindTargetValue(TreeNode node, int target)
        {
            if(node is null)
            {
                return null;
            }

            if (node.val == target)
            {
                return node;
            }

            TreeNode left = FindTargetValue(node.left, target);
            TreeNode right = FindTargetValue(node.right, target);
            if (left is not null)
            {
                return left;
            }
            else
            {
                return right;
            }
        }
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
    #region 1441. Build an Array With Stack Operations
    public static IList<string> BuildArray(int[] target, int n)
    {
        Stack<int> nums = new();
        List<string> operations = new();

        int stackTop = 0;
        bool success = false;
        int targetPointer = 0;
        for (int i = 1; i <= n; i++)
        {
            if (targetPointer == target.Length || nums.SequenceEqual(target))
            {
                break;
            }

            nums.Push(i);
            operations.Add("Push");
            success = nums.TryPeek(out stackTop);

            if (success)
            {
                if (stackTop != target[targetPointer])
                {
                    nums.Pop();
                    operations.Add("Pop");
                }
                else
                {
                    targetPointer++;
                }
            }
        }
        return operations;
    }
    #endregion
    #region 1470. Shuffle the Array
    public int[] Shuffle(int[] nums, int n)
    {
        var numsShuffled = new int[nums.Length];
        int j = 0;
        for (int i = 0; i < n; i++)
        {
            numsShuffled[(i * 2)] = nums[i];
            numsShuffled[(i * 2) + 1] = nums[n + i];
        }
        return numsShuffled;
    }
    #endregion
    #region 1480. Running Sum of 1d Array
    public int[] RunningSum(int[] nums)
    {
        for (int i = 1; i < nums.Length; i++)
        {
            nums[i] = nums[i - 1] + nums[i];
        }
        return nums;
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
    #region 1534. Count Good Triplets
    public int CountGoodTriplets(int[] arr, int a, int b, int c)
    {
        int count = 0;
        for (int i = 0; i + 2 < arr.Length; i++)
        {
            for (int j = i + 1; j + 1 < arr.Length; j++)
            {
                if (Math.Abs(arr[i] - arr[j]) > a)
                {
                    continue;
                }
                for (int k = j + 1; k < arr.Length; k++)
                {
                    if (Math.Abs(arr[j] - arr[k]) > b)
                    {
                        continue;
                    }
                    if (Math.Abs(arr[i] - arr[k]) > c)
                    {
                        continue;
                    }
                    count++;
                }
            }
        }
        return count;
    }
    #endregion
    #region 1603. Design Parking System
    // See ParkingSystem class
    #endregion
    #region 1630. Arithmetic Subarrays
    public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
    {
        int difference = 0;
        int[] subNums = null;
        bool[] validArithmetic = new bool[l.Length];
        Array.Fill(validArithmetic, true);
        for (int i = 0; i < l.Length; i++)
        {
            subNums = nums[l[i]..(r[i] + 1)];
            Array.Sort(subNums);
            difference = Math.Abs(subNums[1] - subNums[0]);
            for (int j = 1; j + 1 < subNums.Length; j++)
            {
                if (Math.Abs(subNums[j + 1] - subNums[j]) != difference)
                {
                    validArithmetic[i] = false;
                }
            }
        }
        return validArithmetic;
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
    #region 1678. Goal Parser Interpretation
    public string Interpret(string command)
    {
        command = command.Replace("()", "o");
        command = command.Replace("(", "");
        command = command.Replace(")", "");
        return command;
    }
    #endregion
    #region 1689. Partitioning Into Minimum Number Of Deci-Binary Numbers
    public int MinPartitions(string n)
    {
        return n.Max() - '0';
    }
    #endregion
    #region 1720. Decode XORed Array
    public int[] Decode(int[] encoded, int first)
    {
        int[] decoded = new int[encoded.Length + 1];
        decoded[0] = first;
        for (int i = 1; i < decoded.Length; i++)
        {
            decoded[i] = decoded[i - 1] ^ encoded[i - 1];
        }
        return decoded;
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
    #region 1823. Find the Winner of the Circular Game
    public int FindTheWinner(int n, int k)
    {
        int i = 0;
        List<int> nums = Enumerable.Range(1, n).ToList();
        while (nums.Count > 1)
        {
            i += k;
            i %= nums.Count;
            nums.RemoveAt(i);
            i--;
        }
        int num = nums[0] - 1;
        return num is 0 ? n : num;
    }
    #endregion
    #region 1844. Replace All Digits with Characters
    public string ReplaceDigits(string s)
    {
        char[] chars = s.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsDigit(chars[i]))
            {
                chars[i] = (char)(chars[i - 1] + (chars[i] - '0'));
            }
        }
        return new string(chars);
    }
    #endregion
    #region 1920. Build Array from Permutation
    public int[] BuildArray(int[] nums)
    {
        var numsPermutation = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            numsPermutation[i] = nums[nums[i]];
        }
        return numsPermutation;
    }
    #endregion
    #region 2011. Final Value of Variable After Performing Operations
    public int FinalValueAfterOperations(string[] operations)
    {
        int num = 0;
        for (int i = 0; i < operations.Length; i++)
        {
            if (operations[i][1] is '+')
            {
                num++;
            }
            else
            {
                num--;
            }
        }
        return num;
    }
    #endregion
    #region 2085. Count Common Words With One Occurrence
    public int CountWords(string[] words1, string[] words2)
    {
        string[] longest = null;
        string[] shortest = null;
        if (words1.Length <= words2.Length)
        {
            shortest = words1;
            longest = words2;
        }
        else
        {
            shortest = words2;
            longest = words1;
        }

        int count = 0;
        for (int i = 0; i < shortest.Length; i++)
        {
            if (shortest.Count(x => x == shortest[i]) is 1 &&
                longest.Count(x => x == shortest[i]) is 1)
            {
                count++;
            }
        }
        return count;
    }
    #endregion
    #region 2103. Rings and Rods
    public int CountPoints(string rings)
    {
        string ringInfo = string.Empty;
        string[] rods = new string[10];
        for (int i = 0; i < rings.Length; i += 2)
        {
            ringInfo = rings[i..(i + 2)];
            rods[ringInfo[1] - '0'] += ringInfo[0];
        }
        return rods.Count(x => x is not null && x.Length > 2 &&
        x.Contains('R') &&
        x.Contains('B') &&
        x.Contains('G'));

    }
    #endregion
    #region 2114. Maximum Number of Words Found in Sentences
    public int MostWordsFound(string[] sentences)
    {
        return sentences.Max(x => x.Count(c => c is ' '));
    }
    #endregion
    #region 2119. A Number After a Double Reversal
    public bool IsSameAfterReversals(int num)
    {
        return num is 0 || num / 10 != num / 10d;
    }
    #endregion
    #region 2164. Sort Even and Odd Indices Independently
    public int[] SortEvenOdd(int[] nums)
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
    #region 2185. Counting Words With a Given Prefix
    public int PrefixCount(string[] words, string pref)
    {
        int count = 0;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].StartsWith(pref))
            {
                count++;
            }
        }
        return count;
    }
    #endregion
    #region 2215. Find the Difference of Two Arrays
    public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
    {
        return new List<int>[] { nums1.Except(nums2).ToList(), nums2.Except(nums1).ToList() };
    }
    #endregion
    #region 2251. Number of Flowers in Full Bloom
    public int[] FullBloomFlowers(int[][] flowers, int[] people)
    {
        int flowerLength = flowers.Length;
        int[] flowersStart = new int[flowerLength];
        int[] flowersEnd = new int[flowerLength];

        for (int i = 0; i < flowerLength; i++)
        {
            flowersStart[i] = flowers[i][0];
            flowersEnd[i] = flowers[i][1];
        }

        Array.Sort(flowersStart);
        Array.Sort(flowersEnd);

        (int t, int i)[] peopleOrdered = new (int,int)[people.Length];
        for (int i = 0; i < peopleOrdered.Length; i++)
        {
            peopleOrdered[i] = (people[i], i);
        }
        peopleOrdered = peopleOrdered.OrderBy(x => x.t).ToArray();

        int numStarted = 0;
        int numEnded = 0;
        int[] flowersBlooming = new int[people.Length];
        for (int i = 0; i < flowersBlooming.Length; i++)
        {
            for (int j = numStarted; j < flowerLength; j++)
            {
                if (flowersStart[j] > peopleOrdered[i].t)
                {
                    break;
                }
                numStarted = j + 1;
            }
            for (int j = numEnded; j < flowerLength; j++)
            {
                if (flowersEnd[j] >= peopleOrdered[i].t)
                {
                    break;
                }
                numEnded = j + 1;
            }
            flowersBlooming[peopleOrdered[i].i] = numStarted - numEnded;
        }
        return flowersBlooming;
    }
    #endregion
    #region 2396. Strictly Palindromic Number
    public bool IsStrictlyPalindromic(int n)
    {
        string numberInBase;
        for (int i = 2; i <= n - 2; i++)
        {
            numberInBase = Convert.ToString(n, i);
            if (numberInBase != string.Format("", numberInBase.Reverse()))
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #region 2427. Number of Common Factors
    public int CommonFactors(int a, int b)
    {
        int factors = 0;
        int maxFactor = Math.Min(a, b);
        for (int i = 1; i <= maxFactor; i++)
        {
            if (a % i is 0 && b % i is 0)
            {
                factors++;
            }
        }
        return factors;
    }
    #endregion
    #region 2433. Find The Original Array of Prefix Xor
    public int[] FindArray(int[] pref)
    {
        int[] array = new int[pref.Length];
        return null;
    }
    #endregion
    #region 2469. Convert the Temperature
    public double[] ConvertTemperature(double celsius)
    {
        return new double[] { celsius + 273.15, celsius * 1.80 + 32.00 };
    }
    #endregion
    #region 2553. Separate the Digits in an Array
    public int[] SeparateDigits(int[] nums)
    {
        return Array.ConvertAll(string.Join("", nums).ToCharArray(), x => x - '0');
    }
    #endregion
    #region 2574. Left and Right Sum Differences
    public int[] LeftRightDifference(int[] nums)
    {
        int[] leftNums = new int[nums.Length];
        int[] rightNums = new int[nums.Length];
        for (int i = 0; i < nums.Length;)
        {
            leftNums[i] = nums[..i].Sum();
            rightNums[i] = nums[++i..].Sum();
        }
        return leftNums.Zip(rightNums, (x, y) => Math.Abs(x - y)).ToArray();
    }
    #endregion
    #region 2605. Form Smallest Number From Two Digit Arrays
    public int MinNumber(int[] nums1, int[] nums2)
    {
        var common = nums1.Intersect(nums2);
        if (common is not null && common.Count() > 0)
        {
            return common.Min();
        }

        int min1 = nums1.Min();
        int min2 = nums2.Min();
        return min1 < min2
            ? int.Parse(string.Join("", min1, min2))
            : int.Parse(string.Join("", min2, min1));
    }
    #endregion
    #region 2710. Remove Trailing Zeros From a String
    public string RemoveTrailingZeros(string num)
    {
        return num.Replace('0', ' ').TrimEnd().Replace(' ', '0');
    }
    #endregion
    #region 2769. Find the Maximum Achievable Number
    public int TheMaximumAchievableX(int num, int t)
    {
        return num + (t * 2);
    }
    #endregion
    #region 2771. Longest Non-decreasing Subarray From Two Arrays
    public int MaxNonDecreasingLength(int[] nums1, int[] nums2)
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
    #region 2859. Sum of Values at Indices With K Set Bits
    public int SumIndicesWithKSetBits(IList<int> nums, int k)
    {
        int sum = 0;
        string binary = string.Empty;
        for (int i = 0; i < nums.Count; i++)
        {
            binary = Convert.ToString(i, 2);
            if (binary.Count(x => x is '1') == k)
            {
                sum += nums[i];
            }
        }
        return sum;
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
public class MountainArray
{
    public int Get(int index) { return 0; }
    public int Length() { return 0; }
}
public class ParkingSystem
{
    int[] spaces = new int[3];
    public ParkingSystem(int big, int medium, int small)
    {
        spaces[0] = big;
        spaces[1] = medium;
        spaces[2] = small;
    }

    public bool AddCar(int carType)
    {
        return --spaces[carType - 1] >= 0;
    }
}
#endregion
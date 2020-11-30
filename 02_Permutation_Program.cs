using System;
using Combinatorics.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PermutationOutput
{
    public partial class Program_permutation
    {
        public static int min_digits; //최소 조합수
        public static int max_digits; //최대 조합수
        public static dynamic[] PWList; //출력용 리스트

        //public static List<string> PWList = new List<string>();
        public static ulong total_PW_numbers = 0; //총 대입 비밀번호 갯수

        //메인
        static Program_permutation()
        {
            Input_Keywords();

            Select_Repeat();

            Set_MinMax_Digits();

            Result_Output();

        }

        static int total_number_of_keyword = 256;
        static string[] keyword = new string[total_number_of_keyword];
        static void Input_Keywords()
        {

            int i;
            for (i = 0; i < total_number_of_keyword; i++)
            {
                Console.Write("{0}번째 키워드를 입력하세요 : ", i + 1);
                keyword[i] = Console.ReadLine();

                while (i == 0)
                {
                    if (keyword[i] == "")
                    {
                        Console.WriteLine("키워드가 입력되지 않았습니다. 다시 입력해주세요.");
                        Console.Write("{0}번째 키워드를 입력하세요 : ", i + 1);
                        keyword[i] = Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }

                }

                if (i == (total_number_of_keyword - 1))
                {
                    Console.WriteLine("입력가능한 {0}개의 키워드를 모두 입력하였습니다.", total_number_of_keyword);
                }

                if (keyword[i] == "")
                {
                    keyword[i] = null;

                    keyword = keyword.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    Console.WriteLine("키워드 입력이 완료되었습니다.");

                    break;
                }


            }

        }

        static bool isRepeat;
        static GenerateOption _isRepeat = GenerateOption.WithRepetition;
        static void Select_Repeat()
        {
            // 조합 출력에서 5개 이하 걸를 예정으로 생략
            //if (keyword.Length < 5)
            //{
            //    Console.WriteLine("키워드 수가 5개 이하이므로 중복순열로 설정됩니다.");
            //    isRepeat = true;
            //    return;
            //}

            while (true)
            {
                Console.WriteLine("중복 여부를 선택하세요.");
                Console.Write("(0 : 중복안함, 1 : 중복함) : ");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    isRepeat = false;
                    break;
                }
                else if (input == "1")
                {
                    isRepeat = true;
                    break;

                }
                else
                {
                    //Nothing here
                }
            }

            if (isRepeat == false)
            {
                _isRepeat = GenerateOption.WithoutRepetition;

            }
            else if (isRepeat == true)
            {
                _isRepeat = GenerateOption.WithRepetition;

            }

        }

        static void Set_MinMax_Digits()
        {
            //Min 설정
            while (true)
            {
                Console.Write("최소 조합 갯수를 입력하세요 : ");
                try
                {
                    min_digits = Convert.ToInt32(Console.ReadLine());
                    if (min_digits < 1)
                    {
                        Console.WriteLine("1 이상의 값을 입력하세요.");
                        continue;

                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("올바른 값을 입력하세요");
                    continue;

                }

            }

            //Max 설정
            while (true)
            {
                Console.Write("최대 조합 갯수를 입력하세요 : ");
                try
                {
                    max_digits = Convert.ToInt32(Console.ReadLine());
                    if (max_digits < 2)
                    {
                        Console.WriteLine("2 이상의 값을 입력하세요.");
                        continue;

                    }
                    if (max_digits < min_digits)
                    {
                        Console.WriteLine("최대 조합 갯수는 최소 조합 갯수보다 커야 합니다.");
                        continue;

                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("올바른 값을 입력하세요");
                    continue;

                }

            }

        }

    }

}

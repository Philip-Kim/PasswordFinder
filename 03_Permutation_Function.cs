using System;
using Combinatorics.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PermutationOutput
{
    public partial class Program_permutation
    {
        static void Result_Output()
        {
            //int order_PWList = 0; //우선 보류

            Variations<string>[] result = new Variations<string>[max_digits+1];

            //enum 메서드 테스트용
            //Variations<string> result1 = new Variations<string>(keyword, 5, GenerateOption.WithoutRepetition);
            //Variations<string>.EnumeratorWithoutRepetition test = new Variations<string>.EnumeratorWithoutRepetition(result1);
            //_ = test.Current;
            
            // 0부터 (min_digits - 1)까지는 우선 버림
            for (int i = min_digits; i <= max_digits; i++)
            {
                result[i] = new Variations<string>(keyword, i, _isRepeat);

            }

            //With, Without 따라 달라짐
            if (isRepeat == false)
            {
                PWList = new Variations<string>.EnumeratorWithoutRepetition[max_digits+1];

                for (int i = min_digits; i <= max_digits; i++)
                {
                    PWList[i] = new Variations<string>.EnumeratorWithoutRepetition(result[i]);

                    total_PW_numbers += (ulong)result[i].Count;

                }

            }
            else if (isRepeat == true)
            {
                PWList = new Variations<string>.EnumeratorWithRepetition[max_digits+1];

                for (int i = min_digits; i <= max_digits; i++)
                {
                    PWList[i] = new Variations<string>.EnumeratorWithRepetition(result[i]);

                    total_PW_numbers += (ulong)result[i].Count;

                }

            }

            ////PWList 배열 크기 증가
            ////total_PW_numbers에 기존 배열크기 저장해뒀다가, for문 시작 전에 우선 추가하고,
            ////아래 foreach문 종료하고 다시 크기 조절

            //string temp;
            //foreach (IList<string> p in result)
            //{
            //    temp = string.Join("", p);

            //    //문자열 길이가 5 미만이면 입력 불가하므로 리스트 추가 안함
            //    if (temp.Length < 5) continue;

            //    PWList[order_PWList] = temp;
            //    //Console.WriteLine(PWList[order_PWList]);
            //    order_PWList++;

            //}

            //남은 크기 줄이고 total_PW_numbers에 반영
            //PWList = PWList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //total_PW_numbers = PWList.Length;

            //확인용
            //for (int i = 0; i < PWList.Length; i++)
            //{
            //    Console.WriteLine(PWList[i]);
            //
            //}

        }

    }

}

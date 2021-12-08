using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Models
{
    /// <summary>
    /// �b��MODEL
    /// </summary>
    public class AccountModels : SetResult
    {
        //�b��
        public string Account { get; set; }

        //�K�X
        public string Password { get; set; }

        //�A���T�{�K�X
        public string PasswordCheck { get; set; }

        //�ʺ�
        public string Username { get; set; }
    }

    /// <summary>
    /// �|��MODEL
    /// </summary>
    public class Member : AccountModels
    {
        //�ʧO
        public string Gender { get; set; }

        //�ͤ�
        public DateTime? Birthday { get; set; }

        //�b���إ߮ɶ�
        public DateTime CreateTime { get; set; }

        //�b���ק�ɶ�
        public DateTime ModifyTime { get; set; }

        //�b���ҥήɶ�
        public DateTime AccountStart { get; set; }

        //�b�����ήɶ�
        public DateTime AccountEnd { get; set; }

        //�������A�s�X
        public string StatusNo { get; set; }
    }

    /// <summary>
    /// ���榨�\�P�_ �˴���Model
    /// </summary>
    public class SetResult
    {
        public SetResult()
        {
            //�w�]
            ok = true;
            ResultMessage = "";
        }

        //�O�_���\
        public bool ok { get; set; }

        //���G�T��
        public string ResultMessage { get; set; }
    }
}
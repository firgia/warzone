using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
using Utils;

public class RulesGameplay : MonoBehaviour
{
    private WeaponController weapon;
    private GameObject[] enemy;   

    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag(TagUtils.Weapon).GetComponent<WeaponController>();
        enemy = GameObject.FindGameObjectsWithTag(TagUtils.Enemy);
    }

    /// <summary>
    /// mendapatkan bintang berdasarkan sisa peluru
    /// </summary>
    /// <returns></returns>
    public int GetStars()
    {
        int maxBullet = weapon.TotalBullet;
        int currentBullet = weapon.CurrentBullet;

        int bulletPerStar = maxBullet / 3;

        /// cek 3 bintang
        if (currentBullet >= (bulletPerStar * 2)) return 3;
        /// cek 2 bintang
        else if (currentBullet >= bulletPerStar && currentBullet < (bulletPerStar * 2)) return 2;
        else return 1;
    }

    /// <summary>
    /// game dianggap selesai jika semua musuh sudah mati atau player tidak mempunyai peluru lagi
    /// </summary>
    /// <returns></returns>
    public bool IsFinishGamePlay()
    {
        return (GetTotalEnemy() == 0 || !HaveBullet());
    }

    /// <summary>
    /// level dianggap selesai jika player sudah membunuh semua musuh
    /// </summary>
    /// <returns></returns>
    public bool LevelPassed()
    {
        return GetTotalEnemy() == 0;
    }

    /// <summary>
    /// level di anggap gagal jika player sudah kehabisan peluru tetapi masih ada musuh
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public bool LevelFailed()
    {
        return GetTotalEnemy() > 0 && !HaveBullet();
    }

    /// <summary>
    /// mendapatkan total musuh yang masih hidup
    /// </summary>
    /// <returns>
    /// jumlah musuh 
    /// </returns>
    public int GetTotalEnemy()
    {
        int totalEnemy = 0;
        for(int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] != null) totalEnemy++;
        }

        return totalEnemy;
    }

    /// <summary>
    /// menentukan apakah pengguna masih mempunyai kesempatan untuk menembak atau masih ada peluru yang sedang aktif untuk membunuh musuh
    /// </summary>
    /// <returns>
    /// true jika ada, false jika tidak ada
    /// </returns>
    private bool HaveBullet()
    {
        if (weapon.CurrentBullet > 0) return true;
        else
        {
            /// player sudah tidak punya peluru
            /// di kondisi ini peluru terakhir sedang memantul ke dinding
            /// peluru yang sedang memantul di catat masih aktif
            GameObject[] bulletInstance = GameObject.FindGameObjectsWithTag(TagUtils.Bullet);
            return bulletInstance.Length > 0;
        }
    }
}

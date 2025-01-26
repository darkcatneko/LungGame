using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BloodWaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject bloodPrefab_;
    [SerializeField] float speedUpValue_;
    void Start()
    {
        spawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void spawner()
    {
        for (int i = 0; i < 50; i++)
        {
            var pos = new Vector3 (15, 0, (-50f+i));
            var obj = Instantiate(bloodPrefab_, pos, quaternion.identity);
            var animator = obj.GetComponent<Animator>();
            animator.SetFloat("Speed",speedUpValue_ * i);
            await UniTask.WaitForSeconds(Mathf.Clamp(( 3f /  i * speedUpValue_),0,1f));
        }
    }
}

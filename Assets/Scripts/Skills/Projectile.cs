using UnityEngine;

namespace PVZ
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 _direction;
        private float _speed;
        private int _damage;
        private UnitEntity _source;
        private UnitEntity _target;
        private bool _hasTarget;

        public void Initialize(UnitEntity source, UnitEntity target, int damage, float speed, Vector3 targetPosition)
        {
            _source = source;
            _target = target;
            _damage = damage;
            _speed = speed;
            _direction = (targetPosition - transform.position).normalized;
            _hasTarget = target != null;
        }

        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
            if (_hasTarget && _target != null)
            {
                if (Vector3.Distance(transform.position, _target.transform.position) <= 0.1f)
                {
                    _target.InflictDamage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}

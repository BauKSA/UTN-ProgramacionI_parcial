using UnityEngine;

public class PlayerHealthStateController : HealthStateController
{
    private HeartsContainerController _heartsContainer;

    private void Start()
    {
        _heartsContainer = HeartsContainerController.Instance;
        _heartsContainer.Initialize(Lives);
    }

    private void Update()
    {
        if (_heartsContainer.GetHearts() == Lives)
            return;

        if(_heartsContainer.GetHearts() > Lives)
        {
            while (_heartsContainer.GetHearts() > Lives)
                _heartsContainer.RemoveHeart();
        }
        else
        {
            while (_heartsContainer.GetHearts() < Lives)
                _heartsContainer.AddHeart();
        }
    }
}
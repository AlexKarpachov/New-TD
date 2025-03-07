using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whereToStart : MonoBehaviour
{
    // пул об'єктів не створює префаби, якщо їх не вистачає в самому пулі, а знищує перші створені.
    // Тобто, якщо хвиля має згенерувати 5 ворогів, але в пулі їх тільки 3, то перший та другий будуть знищенні для створення 4го та 5го ворогів.
}

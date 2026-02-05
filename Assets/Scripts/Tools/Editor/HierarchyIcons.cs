using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public class HierarchyIcons
{
    // Статический конструктор вызывается при загрузке редактора
    static HierarchyIcons()
    {
        // Подписываемся на событие отрисовки строки в иерархии
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        // Получаем объект по его ID
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj == null) return;

        // Получаем все компоненты на объекте
        Component[] components = obj.GetComponents<Component>();

        // Начальная позиция для отрисовки (сдвигаем влево от правого края)
        // selectionRect.width + selectionRect.x дает правый край
        float currentX = selectionRect.x + selectionRect.width - 20;

        // Проходимся по компонентам в обратном порядке (чтобы первый компонент был правее всех или левее - на ваш вкус)
        // Здесь идем с конца списка, чтобы Transform (который обычно первый) рисовался последним (или мы его вообще пропустим)
        for (int i = components.Length - 1; i >= 0; i--)
        {
            Component c = components[i];

            // Пропускаем null (например, missing script)
            if (c == null) continue;

            // --- Фильтры (что НЕ отображать) ---

            // Обычно Transform есть у всех, его иконка создает шум, лучше скрыть
            if (c is Transform || c is RectTransform) continue;

            // Можно скрыть CanvasRenderer для UI, если мешает
            // if (c is CanvasRenderer) continue;

            // -----------------------------------

            // Получаем иконку компонента
            GUIContent content = EditorGUIUtility.ObjectContent(c, c.GetType());

            // Если у компонента нет иконки, пропускаем
            if (content.image == null) continue;

            // Рисуем иконку
            // Rect(x, y, width, height)
            Rect iconRect = new Rect(currentX, selectionRect.y, 16, 16);

            // Отрисовка
            GUI.Label(iconRect, new GUIContent(content.image, c.GetType().Name));

            // Сдвигаем позицию для следующей иконки влево
            currentX -= 18;
        }
    }
}
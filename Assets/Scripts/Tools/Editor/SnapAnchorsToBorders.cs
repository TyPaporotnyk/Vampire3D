#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;

public static class SnapUIHotkeys
{
    // ---------- Shortcuts (Shortcut Manager) ----------
    // Ctrl/Cmd + [  -> Snap Anchors To Borders
    [Shortcut("Tools/UI/Snap Anchors To Borders", KeyCode.LeftBracket, ShortcutModifiers.Action)]
    private static void Shortcut_SnapAnchors()
    {
        SnapAnchorsToBorders();
    }

    // Ctrl/Cmd + ]  -> Snap Corners To Anchors
    [Shortcut("Tools/UI/Snap Corners To Anchors", KeyCode.RightBracket, ShortcutModifiers.Action)]
    private static void Shortcut_SnapCorners()
    {
        SnapCornersToAnchors();
    }

    // ---------- Menu items (fallback / visible in menu) ----------
    [MenuItem("Tools/UI/Snap Anchors To Borders %#&a")] // visible in menu; default: Ctrl+Shift+Alt+A (user can rebind)
    private static void Menu_SnapAnchors() => SnapAnchorsToBorders();

    [MenuItem("Tools/UI/Snap Corners To Anchors %#&b")]
    private static void Menu_SnapCorners() => SnapCornersToAnchors();

    // ---------- Implementation ----------
    private static void SnapAnchorsToBorders()
    {
        var transforms = Selection.transforms;
        if (transforms == null || transforms.Length == 0)
        {
            Debug.LogWarning("No UI element selected.");
            return;
        }

        foreach (var tr in transforms)
        {
            RectTransform rt = tr as RectTransform;
            if (rt == null)
            {
                Debug.LogWarning($"Skipped {tr.name}: not a RectTransform.");
                continue;
            }

            if (rt.parent == null)
            {
                Debug.LogWarning($"Skipped {tr.name}: has no parent.");
                continue;
            }

            RectTransform parent = rt.parent as RectTransform;
            Vector2 parentSize = parent.rect.size;
            if (Mathf.Approximately(parentSize.x, 0f) || Mathf.Approximately(parentSize.y, 0f))
            {
                Debug.LogWarning($"Skipped {tr.name}: parent has zero size.");
                continue;
            }

            Undo.RecordObject(rt, "Snap Anchors To Borders");

            Vector2 offsetMin = rt.offsetMin;
            Vector2 offsetMax = rt.offsetMax;
            Vector2 anchorMin = rt.anchorMin;
            Vector2 anchorMax = rt.anchorMax;

            Vector2 newAnchorMin = new Vector2(
                anchorMin.x + offsetMin.x / parentSize.x,
                anchorMin.y + offsetMin.y / parentSize.y
            );

            Vector2 newAnchorMax = new Vector2(
                anchorMax.x + offsetMax.x / parentSize.x,
                anchorMax.y + offsetMax.y / parentSize.y
            );

            // clamp anchors to [0,1] to avoid crazy values
            newAnchorMin.x = Mathf.Clamp01(newAnchorMin.x);
            newAnchorMin.y = Mathf.Clamp01(newAnchorMin.y);
            newAnchorMax.x = Mathf.Clamp01(newAnchorMax.x);
            newAnchorMax.y = Mathf.Clamp01(newAnchorMax.y);

            rt.anchorMin = newAnchorMin;
            rt.anchorMax = newAnchorMax;

            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            EditorUtility.SetDirty(rt);
            Debug.Log($"Anchors snapped for {rt.name}");
        }
    }

    private static void SnapCornersToAnchors()
    {
        var transforms = Selection.transforms;
        if (transforms == null || transforms.Length == 0)
        {
            Debug.LogWarning("No UI element selected.");
            return;
        }

        foreach (var tr in transforms)
        {
            RectTransform rt = tr as RectTransform;
            if (rt == null)
            {
                Debug.LogWarning($"Skipped {tr.name}: not a RectTransform.");
                continue;
            }

            Undo.RecordObject(rt, "Snap Corners To Anchors");

            // Если нужно, чтобы rect занимал ровно область между anchors — offsets = 0
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            EditorUtility.SetDirty(rt);
            Debug.Log($"Corners snapped for {rt.name}");
        }
    }
}
#endif
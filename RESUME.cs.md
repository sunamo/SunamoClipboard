---
schema_version: 2
type: library
file_count: 26
delete_recommendation_percent: 15
generated_date: 2026-09-30
generated_time: 15:11:08
---

## Description

Knihovna pro práci se schránkou: `ClipboardHelper` (čtení a zápis textu přes TextCopy), `ClipboardMonitor` (sledování změn schránky) a nativní P/Invoke volání (`ClipboardNative`). Obsahuje polyfilly pro net48.
Balíček je self-contained: P/Invoke deklarace a pomocné string helpery jsou zkopírované lokálně (`_sunamo\`), takže nereferencuje jiné Sunamo balíčky.

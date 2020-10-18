# Unity Package Example

## Installation

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

Use stable version
```json
{
    "dependencies": {
        "com.ensign.unity": "1.0.0"
    },
    "scopedRegistries": [{
        "name": "Ensign Studio",
        "url": "http://dungnv.info:4873",
        "scopes": [
            "com.ensign"
        ]
    }]
}
```

Use github or local for debugging
```json
{
    "dependencies": {
        "com.ensign.unity": "https://github.com/vietdungvn88/ensign-unity.git#package",
        "com.ensign.unity": "file:D:/Projects/EnsignLib/Ensign.Unity.Package/2018.4",
        ...
    }
}
```
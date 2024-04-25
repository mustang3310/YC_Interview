document.addEventListener(
    "DOMContentLoaded",
    () => {
        var element = document.querySelector("trix-editor");
        element.editor.element.editorController.toolbarController.updateActions({
            decreaseNestingLevel: true,
            increaseNestingLevel: true,
            attachFiles: false
        });

        document.getElementsByClassName("trix-button trix-button--icon trix-button--icon-attach")[0].style.display = "none";
    });

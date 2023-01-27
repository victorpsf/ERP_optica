<template>
    <div
        class="selection-file-field noselect"
    >
        <div
            class="file-input UI-border"
        >
            <div :class="labelPosition">{{ label }}</div>
            <div
                class="svg-click"
                @click="InputFiles"
            >
                <svg-image
                    name="upload"
                    width="15px"
                    height="15px"
                />
            </div>
            <div
                class="files"
            >
                <div
                    v-for="(_file, index) in files"
                    :class="`file UI-border ${_file.success ? 'success': 'error'}`"
                    :key="index"
                >
                    <div 
                        class="file-action"
                        @click="RemoveFile($event, _file, index)"
                    >
                        <svg-image
                            name="close"
                            width="10px"
                            height="10px"
                        />
                    </div>

                    <div class="file-info">
                        <div>{{ _file.name }}</div>
                        <div>{{ _file.size }}</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="error-messages" v-if="errorMessages.length > 0">
            <ul>
                <li v-for="(message, index) in errorMessages" :key="index">
                    {{ message }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script src="@/components/fields/selections/file/File.js" lang="js"></script>
<style src="@/components/fields/selections/file/File.scss" lang="scss"></style>